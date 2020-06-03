let buscaHTML = $('input#buscarAnamnese');
//let registroPorPaginaHTML = $('select#RegistroPorPagina');
let url = '/Anamneses/ListAnamneses';
let numeroPaginaHTML = $('li.page-item.active');
let totalPagString = 'input[id="Pagination_TotalPages"]';
let totalRegString = 'input[id="Pagination_TotalRecords"]';
ConstructorUrl(url);
ConstructorAppView(buscaHTML, numeroPaginaHTML, null);
ConstructorPagination(totalPagString, totalRegString)