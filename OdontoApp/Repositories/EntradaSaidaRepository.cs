using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models.Const;
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class EntradaSaidaRepository : IEntradaSaidaRepository
    {
        private readonly OdontoAppContext context;

        public EntradaSaidaRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(EntradaSaida entity)
        {
            await context.EntradaSaida.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckEntityAsync(EntradaSaida entity)
        {
            return await context.EntradaSaida.AnyAsync(EoS => EoS.EntradaSaidaId == entity.EntradaSaidaId);
        }

        public async Task DeleteAsync(EntradaSaida entity)
        {
            context.EntradaSaida.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<PaginationList<EntradaSaida>> GetAllAsync(AppView appQuery, int idUser)
        {
            var pagList = new PaginationList<EntradaSaida>();
            var entradasaidas = context.EntradaSaida.Include(eos => eos.Estoque).ThenInclude(eos => eos.Produto).Where(eos => eos.Estoque.Produto.UsuarioId == idUser).AsNoTracking().AsQueryable();
            if (appQuery.CheckDate())
            {
                entradasaidas = entradasaidas.Where(eos => eos.DataTransacao >= appQuery.Start && eos.DataTransacao <= appQuery.End);
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await entradasaidas.CountAsync();
                entradasaidas = entradasaidas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await entradasaidas.OrderByDescending(eos => eos.DataTransacao).ToListAsync());
            return pagList;
        }

        public async Task<PaginationList<EntradaSaida>> GetAllInputsAsync(AppView appQuery, int idUser)
        {
            var pagList = new PaginationList<EntradaSaida>();
            var entradasaidas = context.EntradaSaida.
                                Include(eos => eos.Estoque).
                                    ThenInclude(eos => eos.Produto).
                                Where(eos => eos.Estoque.Produto.UsuarioId == idUser && eos.TransactionType == TransactionType.Input).
                                AsNoTracking().
                                AsQueryable();

            if (appQuery.CheckDate())
            {
                entradasaidas = entradasaidas.Where(eos => eos.DataTransacao >= appQuery.Start && eos.DataTransacao <= appQuery.End);
            }

            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await entradasaidas.CountAsync();
                entradasaidas = entradasaidas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }

            pagList.AddRange(await entradasaidas.OrderByDescending(eos => eos.DataTransacao).ToListAsync());
            return pagList;
        }

        public async Task<PaginationList<EntradaSaida>> GetAllOutputsAsync(AppView appQuery, int idUser)
        {
            var pagList = new PaginationList<EntradaSaida>();
            var entradasaidas = context.EntradaSaida.
                                Include(eos => eos.Estoque).
                                    ThenInclude(eos => eos.Produto).
                                Where(eos => eos.Estoque.Produto.UsuarioId == idUser && eos.TransactionType == TransactionType.Output).
                                AsNoTracking().
                                AsQueryable();

            if (appQuery.CheckDate())
            {
                entradasaidas = entradasaidas.Where(eos => eos.DataTransacao >= appQuery.Start && eos.DataTransacao <= appQuery.End);
            }

            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await entradasaidas.CountAsync();
                entradasaidas = entradasaidas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }

            pagList.AddRange(await entradasaidas.OrderByDescending(eos => eos.DataTransacao).ToListAsync());
            return pagList;
        }
        public async Task<EntradaSaida> GetByIdAsync(int id, int idUser)
        {
            return await context.EntradaSaida.Include(obj => obj.Estoque).ThenInclude(obj => obj.Produto).Where(eos => eos.EntradaSaidaId == id && eos.Estoque.Produto.UsuarioId == idUser).FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(EntradaSaida entity)
        {
            try
            {
                context.EntradaSaida.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
