using OdontoApp.Models.Const;
using OdontoApp.Models.Estoque;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using OdontoApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdontoApp.Services
{
    public class ProdutoService: IProdutoService
    {
        private readonly IProdutoRepository produtoRepos;
        private readonly IAuthService authService;

        public ProdutoService(IProdutoRepository produtoRepos, IAuthService authService)
        {
            this.produtoRepos = produtoRepos;
            this.authService = authService;
        }
        public async Task AddAsync(Produto entity)
        {
            entity.UsuarioId = authService.GetLoggedUserAsync().Result.Id;
            await produtoRepos.AddAsync(entity);
        }

        public async Task<bool> CheckEntityAsync(Produto entity) => await produtoRepos.CheckEntityAsync(entity);

        public async Task DeleteAsync(int id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task DeleteAsync(Produto entity)
        {
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await produtoRepos.DeleteAsync(entity);
        }

        public async Task<List<Produto>> GetAllAsync() => await produtoRepos.GetAllAsync(new AppView(), authService.GetLoggedUserAsync().Result.Id);

        public async Task<Produto> GetByIdAsync(int id) => await produtoRepos.GetByIdAsync(id, authService.GetLoggedUserAsync().Result.Id);

        public async Task<PaginationList<Produto>> GetAllAsync(AppView appQuery)
        {
            appQuery.RecordPerPage ??= NumElement.NumElements;
            appQuery.NumberPag ??= 1;
            return await produtoRepos.GetAllAsync(appQuery, authService.GetLoggedUserAsync().Result.Id);
        }
        public async Task UpdateAsync(Produto entity)
        {
            if (!await CheckEntityAsync(entity))
                throw new NotFoundException("Produto não existe");
            if (entity.UsuarioId == authService.GetLoggedUserAsync().Result.Id)
                await produtoRepos.UpdateAsync(entity);
        }
    }
}
