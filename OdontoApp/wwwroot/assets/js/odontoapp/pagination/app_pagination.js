let startHTML,
    searchHTML,
    endHTML,
    numberPageHTML,
    recordPerPageHTML,
    totalPagesString,
    totalRecordsString,
    currentPage = 1,
    visiblePages = 2,
    urlPag,
    timer = null;

const ulPage = $('ul.pagination');
const divLoading = $('div#loading');
const divElements = $('div#ElementsList');

function AddFunction() {
    if (searchHTML != null) {
        searchHTML.keydown(debounce(RefreshPage, 500));
    }
    if (recordPerPageHTML != null) {
        recordPerPageHTML.change(debounce(RefreshPage, 0));
    }
    if (endHTML != null && startHTML != null) {
        endHTML.change(debounce(RefreshPage, 0));
        startHTML.change(debounce(RefreshPage, 0));
    }
}

function AppViewModel(appView) {
    $.ajax({
        type: "GET",
        url: urlPag,
        async: true,
        data: appView,
        contentType: 'application/json',
        success: function (partialView) {
            divLoading.removeClass('d-flex');
            divLoading.addClass('d-none');
            divElements.show();
            divElements.html(partialView);
            AddLiElementsPage();
        },
        error: function (code) {
            divElements.html(`<h4 class="display-3 mt-5 text-center font-weight-bold">Algo deu errado Erro${code.status}, se o problema persistir entre em contato conosco</h4>`);
        }
    })
}

function ConstructorPagination(totalPagesArg = null, totalRecordsArg = null) {
    totalPagesString = totalPagesArg;
    totalRecordsString = totalRecordsArg;
}

function ConstructorAppView(searchArg = null, numberpageArg = null, recordperpageArg = null, startArg = null, endArg = null) {
    startHTML = startArg;
    searchHTML = searchArg;
    endHTML = endArg;
    numberPageHTML = numberpageArg;
    recordPerPageHTML = recordperpageArg;
    AddFunction();
    RefreshPage();
}

function ConstructorUrl(url) {
    urlPag = url;
}

function debounce(func, wait) {
    return function () {
        currentPage = 1;
        clearTimeout(timer);
        timer = setTimeout(func, wait);
    }
}

function ChangePage(changedPage) {

    ulPage.find('li.page-item.active').removeClass('active');
    ulPage.find(`li.page-item#${changedPage}`).addClass('active');

    currentPage = Number(changedPage);
    RefreshPage();
}

function AddLiElementsPage() {
    let previousPage = currentPage - visiblePages;
    let nextPage = currentPage + visiblePages;

    CurrentPage(currentPage);
    PreviousPages(previousPage);
    NextPages(nextPage);
}

function PreviousPages(previousPage) {
    if (currentPage > 1)
        ulPage.find('li.page-item.active').before(`<li class="page-item" id="1"><a class="page-link text-primary" href="#" onclick ="ChangePage(1)"><i class="fa fa-angle-double-left"></i></a></li>`);
    for (previousPage; previousPage <= currentPage - 1; previousPage++) {
        if (previousPage >= 1) {
            ulPage.find('li.page-item.active').before(`<li class="page-item" id="${previousPage}"><a class="page-link text-primary font-weight-bold" href="#" onclick ="ChangePage(${previousPage})">${previousPage}</a></li>`);
        }
    }
}

function CurrentPage(currentPage) {
    if (Number($(totalRecordsString).val()) > 0)
        ulPage.append(`<li class="page-item active" id="${currentPage}"><span class="page-link font-weight-bold">${currentPage}</span></li>`);
}

function NextPages(nextPage) {
    for (nextPage; nextPage >= currentPage + 1; nextPage--) {
        if (nextPage <= Number($(totalPagesString).val())) {
            ulPage.find('li.page-item.active').after(`<li class="page-item" id="${nextPage}"><a class="page-link text-primary font-weight-bold" href="#" onclick ="ChangePage(${nextPage})">${nextPage}</a></li>`);
        }
    }
    let totalPages = parseInt($(totalPagesString).val())
    if (totalPages > 1 && currentPage < totalPages)
        ulPage.append(`<li class="page-item" id="${totalPages}"><a class="page-link text-primary" href="#" onclick ="ChangePage(${totalPages})"><i class="fa fa-angle-double-right"></i></a></li>`);
}

function RefreshPage() {

    divElements.hide();
    divLoading.removeClass('d-none');
    divLoading.addClass('d-flex');
    ulPage.find('li.page-item').remove();

    let appView = {
        Search: String(searchHTML.val()) || null,
        NumberPag: currentPage,
        RecordPerPage: parseInt(recordPerPageHTML.val()) || null,
        Start: startHTML != null ? Date.parse(startHTML.val()) : null,
        End: endHTML != null ? Date.parse(endHTML.val()) : null
    };

    AppViewModel(appView);
}

