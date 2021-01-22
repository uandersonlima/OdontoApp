using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class PerguntaAnamneseRepository : IPerguntaAnamneseRepository
    {
        private readonly OdontoAppContext context;

        public PerguntaAnamneseRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(PerguntaAnamnese entity)
        {
            await context.PerguntaAnamnese.AddAsync(entity);
            await context.SaveChangesAsync(); ;
        }
        public async Task<bool> CheckEntityAsync(PerguntaAnamnese entity)
        {
            return await context.PerguntaAnamnese.AnyAsync(cnc => cnc.PerguntaAnamneseId == entity.PerguntaAnamneseId);
        }
        public async Task DeleteAsync(PerguntaAnamnese entity)
        {
            context.AnamnesesPerguntas.RemoveRange(context.AnamnesesPerguntas.Where(obj => obj.PerguntaAnamneseId == entity.PerguntaAnamneseId));
            context.PerguntaAnamnese.Remove(entity);
            context.Pergunta.Remove(entity.Pergunta);
            await context.SaveChangesAsync();
        }
        public async Task<PerguntaAnamnese> GetByIdAsync(int id, string userId) => await context.PerguntaAnamnese.Include(x => x.TipoPergunta).Include(x => x.Pergunta).Where(cnc => cnc.PerguntaAnamneseId == id && cnc.UsuarioId == userId).FirstOrDefaultAsync();
        public async Task<PaginationList<PerguntaAnamnese>> GetAllAsync(AppView appQuery, string userId)
        {
            var pagList = new PaginationList<PerguntaAnamnese>();
            var perguntasanamnese = context.PerguntaAnamnese.Include(p => p.TipoPergunta).Include(p => p.Pergunta).Where(cnc => !cnc.RespostaId.HasValue && cnc.UsuarioId == userId).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                perguntasanamnese = perguntasanamnese.Where(cnc => cnc.Pergunta.DescricaoPergunta.Contains(appQuery.Search.Trim()));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await perguntasanamnese.CountAsync();
                perguntasanamnese = perguntasanamnese.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await perguntasanamnese.ToListAsync());

            return pagList;
        }
        public async Task UpdateAsync(PerguntaAnamnese entity)
        {
            try
            {
                context.PerguntaAnamnese.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
