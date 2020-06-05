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
    public class AnamneseRepository : IAnamneseRepository
    {
        private readonly OdontoAppContext context;

        public AnamneseRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Anamnese entity)
        {
            await context.Anamnese.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CheckEntityAsync(Anamnese entity)
        {
            return await context.Anamnese.AnyAsync(anm => anm.AnamneseId == entity.AnamneseId);
        }

        public async Task DeleteAllAsync(Anamnese entity)
        {
            context.RemoveRange(entity.AnamnesesPerguntas.Select(AoP => AoP.PerguntaAnamnese.Resposta));
            context.RemoveRange(entity.AnamnesesPerguntas.Select(AoP => AoP.PerguntaAnamnese));
            context.RemoveRange(entity.AnamnesesPerguntas);
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Anamnese entity)
        {
            context.RemoveRange(entity.AnamnesesPerguntas);
            context.Anamnese.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<PaginationList<Anamnese>> GetAllAsync(AppView appQuery, int idUser)
        {
            var pagList = new PaginationList<Anamnese>();
            var anamneses = context.Anamnese.Where(anm => anm.UsuarioId == idUser && !anm.PacienteId.HasValue).AsNoTracking().AsQueryable();
            if (appQuery.CheckSearch())
            {
                anamneses = anamneses.Where(anm => anm.DescricaoAnamnese.Contains(appQuery.Search));
            }
            if (appQuery.CheckPagination())
            {
                var quantidadeTotalRegistros = await anamneses.CountAsync();
                anamneses = anamneses.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

                var paginacao = new Pagination
                {
                    NumberPag = appQuery.NumberPag.Value,
                    RecordPerPage = appQuery.RecordPerPage.Value,
                    TotalRecords = quantidadeTotalRegistros,
                    TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
                };

                pagList.Pagination = paginacao;
            }
            pagList.AddRange(await anamneses.OrderBy(obj => obj.DescricaoAnamnese).ToListAsync());
            return pagList;
        }

        public async Task<Anamnese> GetByIdAsync(int id, int idUser)
        {
            return await context.Anamnese.
                        Include(anm => anm.Usuario).
                        Include(anm => anm.AnamnesesPerguntas).
                            ThenInclude(anamnesesPerguntas => anamnesesPerguntas.PerguntaAnamnese).
                                ThenInclude(perguntAnammnese => perguntAnammnese.TipoPergunta).
                        Include(anm => anm.AnamnesesPerguntas).
                            ThenInclude(anamnesesPerguntas => anamnesesPerguntas.PerguntaAnamnese).
                                ThenInclude(perguntaAnamneses => perguntaAnamneses.Pergunta).
                        Where(anm => anm.UsuarioId == idUser && !(anm.PacienteId.HasValue) && anm.AnamneseId == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Anamnese entity)
        {
            try
            {
                var pA = await context.AnamnesesPerguntas.Where(p => p.AnamneseId == entity.AnamneseId).ToListAsync();
                context.AnamnesesPerguntas.RemoveRange(pA);
                context.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
