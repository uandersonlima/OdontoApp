using Microsoft.EntityFrameworkCore;
using OdontoApp.Data;
using OdontoApp.Models;
using OdontoApp.Models.Helpers;
using OdontoApp.Repositories.Interfaces;
using OdontoApp.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly OdontoAppContext context;

        public MessageRepository(OdontoAppContext context)
        {
            this.context = context;
        }

        public async Task DeleteMessageAsync(Message msg)
        {
            context.Message.Remove(msg);
            await context.SaveChangesAsync();
        }

        public async Task<Message> GetMessageAsync(Guid messagecode, string senderId)
        {
            return await context.Message.Where(msg => msg.Messagecode == messagecode && senderId == msg.SenderUserId).FirstOrDefaultAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(AppView appview, string senderId)
        {
            //var pagList = new PaginationList<Agenda>();
            //var agendas = context.Agenda.Where(agd => agd.UsuarioId == idUser).AsNoTracking().AsQueryable();
            //if (appQuery.CheckDate())
            //{
            //    agendas = agendas.Where(agd => agd.Inicio >= appQuery.Start && agd.Inicio <= appQuery.End);
            //}
            //if (appQuery.CheckPagination())
            //{
            //    var quantidadeTotalRegistros = await agendas.CountAsync();
            //    agendas = agendas.Skip((appQuery.NumberPag.Value - 1) * appQuery.RecordPerPage.Value).Take(appQuery.RecordPerPage.Value);

            //    var paginacao = new Pagination
            //    {
            //        NumberPag = appQuery.NumberPag.Value,
            //        RecordPerPage = appQuery.RecordPerPage.Value,
            //        TotalRecords = quantidadeTotalRegistros,
            //        TotalPages = (int)Math.Ceiling((double)quantidadeTotalRegistros / appQuery.RecordPerPage.Value)
            //    };

            //    pagList.Pagination = paginacao;
            //}
            //pagList.AddRange(await agendas.ToListAsync());

            //return pagList;
            return await context.Message.Where(msg => msg.SenderUserId == senderId).ToListAsync();
        }

        public async Task<List<Message>> GetUnreadMessagesAsync(string senderId)
        {
            return await context.Message.Where(msg => msg.SenderUserId == senderId && !msg.ViewedTime.HasValue).ToListAsync();
        }

        public async Task SendMessageAsync(Message msg)
        {
            await context.Message.AddAsync(msg);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMessageAsync(Message msg)
        {
            try
            {
                context.Message.Update(msg);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<List<string>> UserListAsync(string userId)
        {
            var x = await context.Message.Where(msg => msg.ReceiverUserId == userId || msg.SenderUserId == userId).ToListAsync();
            var userListId = new List<string>();
            x.ForEach(msg => userListId.AddRange(new string[]{msg.SenderUserId, msg.ReceiverUserId}));
            userListId = userListId.Distinct().ToList();
            userListId.Remove(userId);
            return userListId;
        }
    }
}
