﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdontoApp.Libraries.Filtro;
using OdontoApp.Libraries.Lang;
using OdontoApp.Models;
using OdontoApp.Models.APICartao;
using OdontoApp.Services.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OdontoApp.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly ICodigoPromocionalService codepromoSvc;
        private readonly IConfiguration conf;
        private readonly IEnderecoService enderecoSvc;
        private readonly IAuthService authService;
        private readonly IUsuarioService userSvc;
        public PagamentosController(ICodigoPromocionalService codepromoSvc,
                                        IConfiguration conf,
                                            IEnderecoService enderecoSvc,
                                                IAuthService authService,
                                                    IUsuarioService userSvc)
        {
            this.codepromoSvc = codepromoSvc;
            this.conf = conf;
            this.enderecoSvc = enderecoSvc;
            this.authService = authService;
            this.userSvc = userSvc;
        }

        [HttpGet, Route("Cartao")]
        public async Task<IActionResult> DadosDoCartao()
        {
            if (await authService.GetLoggedUserAsync() is null)
            {
                return View();
            }
            return RedirectToAction("signin", "auth");
        }

        [HttpPost]
        public async Task<IActionResult> GerarAssinatura(string hash, string codigoPromocional)
        {
            var codigo = await codepromoSvc.VerificarCodigoPromocional(codigoPromocional);
            //nessa ection, você vai buscar os dados salvos no BD , tranforma-los em string, pois o post é em JSON
            var Hash = hash; //Não precisa armazenar isso
            if (hash != null)
            {
                var usuarioAux = await userSvc.GetByIdAsync(authService.GetLoggedUserAsync().Result.Id);
                Endereco endereco = await enderecoSvc.GetFullAdressByIdAsync(usuarioAux.EnderecoId);
                string URI = "https://api.pagar.me/1/subscriptions";
                NovaAssinatura novaAssinatura = new NovaAssinatura
                {
                    api_key = conf.GetConnectionString("APIKEY"),
                    card_hash = hash,
                    customer = new Customer
                    {
                        address = new Address
                        {
                            zipcode = endereco.Cep.Descricao,
                            neighborhood = endereco.Cidade.Descricao,
                            street = endereco.Rua.Descricao,
                            street_number = endereco.NumeroEndereco,
                        },
                        document_number = usuarioAux.CPF,
                        email = usuarioAux.Email,
                        name = usuarioAux.Nome
                    },
                    phone = new Phone { ddd = usuarioAux.PhoneNumber.Substring(0,2), number = usuarioAux.PhoneNumber.Substring(2)},
                    payment_method = "credit_card",
                    plan_id = codigo,
                    postback_url = "https://localhost:44364/clientes"

                };
                //aqui não precisa modificar nada so armazenar no Bd a variavel ID, pois srvirá para manipularmos as tranzações
                using (var cliente = new HttpClient())
                {
                    var serial = JsonConvert.SerializeObject(novaAssinatura);
                    var content = new StringContent(serial, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var result = await cliente.PostAsync(URI, content);
                    string responseString = await result.Content.ReadAsStringAsync();
                    var Jobject = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                    var id = (string)Jobject["id"]; //tem que guardar esse ID, pois ele será utilizado para tudo que formos fazer
                    if (id == null)
                    {
                        TempData["MSG_E"] = Mensagem.MSG_E012;
                        return RedirectToAction(nameof(DadosDoCartao));
                    }
                    usuarioAux.PlanNumber = id;
                    await userSvc.UpdateAsync(usuarioAux);
                }
                await authService.SignInAsync(usuarioAux);
                return RedirectToAction(nameof(ConsultarAssinaturas));
            }
            return RedirectToAction(nameof(DadosDoCartao));

        }

        [HttpGet]
        public async Task<IActionResult> ConsultarAssinaturas()
        {
            var loggedUser = await authService.GetLoggedUserAsync();
            if (!(loggedUser is null))
            {
                var usuario = await userSvc.GetByIdAsync(loggedUser.Id);
                if (usuario.PlanNumber == null)
                {
                    TempData["MSG_E"] = "O cartão cadastrado não é mais um cartão valido!";
                    return RedirectToAction(nameof(DadosDoCartao));
                }
                string URI = "https://api.pagar.me/1/subscriptions/" + usuario.PlanNumber; //No lugar do 421621 tu vai colocar o ID que tu salvou no BD na ação gerar assinatura recebendo como parametro o usuario
                string urlParameters = "?api_key=" + conf.GetConnectionString("APIKEY");

                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(URI);
                    cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var result = await cliente.GetAsync(urlParameters);
                    string responseString = await result.Content.ReadAsStringAsync();
                    var Jobject = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                    var status = (string)Jobject["status"]; //Essa variavel deve servir como parametro para dar ou não acesso as outras Actions na implementação do identity, logo deve ser armazenar ele e verifixar-se a cada 30 dias ou diariamente caso esteja pendente, pois ela nós diz se a assinatura foi ou não paga
                    var usuarioaux = await userSvc.GetByIdAsync(usuario.Id);
                    if (usuarioaux.PlanNumber == usuario.PlanNumber)
                    {
                        usuario.PaymentStatus = status;
                        await userSvc.UpdateAsync(usuario);
                    }
                }
                await authService.SignInAsync(usuario);
                return RedirectToAction("Index", "Agendas");
            }
            return RedirectToAction(nameof(Index));
        }

        [UserAuthorization]
        public IActionResult CancelarPagamento()
        {
            return PartialView("_CancelarPagamento");
        }


        [HttpPost, UserAuthorization]
        public async Task<IActionResult> CancelarAssinatura()
        {
            var usuario = await userSvc.GetByIdAsync(authService.GetLoggedUserAsync().Result.Id);
            string URI = $"https://api.pagar.me/1/subscriptions/{usuario.PlanNumber}/cancel"; //colocar no {0} o Numero do plano do cliente
            string urlParameters = "?api_key=" + conf.GetConnectionString("APIKEY");

            using (var cliente = new HttpClient())
            {

                cliente.BaseAddress = new Uri(URI);
                cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent("application/json");
                var result = await cliente.PostAsync(urlParameters, content);
                string responseString = await result.Content.ReadAsStringAsync();
                var Jobject = Newtonsoft.Json.Linq.JObject.Parse(responseString);
                var status = (string)Jobject["status"];
                usuario.PaymentStatus = status;
                await userSvc.UpdateAsync(usuario);
            }
            await authService.SignInAsync(usuario);
            return RedirectToAction("Index", "Usuarios");
        }
    }
}