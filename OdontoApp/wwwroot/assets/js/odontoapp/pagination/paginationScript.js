let startHTML,
    searchHTML,
    endHTML,
    numberPageHTML,
    recordPerPageHTML,
    totalPagesString,
    totalRecordsString,
    currentPage = 1,
    visiblePages = 3,
    urlPag;

const  ulPage = $('ul.pagination');
const divLoading = $('div#loading');
const divElements = $('div#ElementsList');

function AddFunction() {
    if (searchHTML != null) {
        searchHTML.keypress(debounce(UpdatePage, 500));
    }
    if (recordPerPageHTML != null) {
        recordPerPageHTML.change(debounce(UpdatePage, 0));
    }
    if (endHTML != null && startHTML != null) {
        endHTML.change(debounce(UpdatePage, 0))
        startHTML.change(debounce(UpdatePage, 0))
    }
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
    UpdatePage();
}

function ConstructorUrl(url) {
    urlPag = url;
}

function debounce(func, wait) {
    let timer = null;
    return function () {
        clearTimeout(timer);
        timer = setTimeout(func, wait);
    }
}

function UpdatePage() {
    ulPage.find('li.page-item').remove();
    divLoading.removeClass('d-none')
    divLoading.addClass('d-flex')
    let appView = {
        Search: String(searchHTML.val()) || null,
        NumberPag: Number(currentPage),
        RecordPerPage: parseInt(recordPerPageHTML.val()) || null,
        Start: startHTML != null ? Date.parse(startHTML.val()) : null,
        End: endHTML != null ? Date.parse(endHTML.val()) : null
    }
    currentPage = appView.NumberPag;
    console.log(appView);
    AppViewModel(appView);
}

function ChangePage(changedPage) {
    $('li.page-item.active').removeClass('active');
    $(`li.page-item#${changedPage}`).addClass('active');
    currentPage = changedPage;
    UpdatePage();
}

function RecordPerPages() {
    return Number(recordPerPageHTML.val());
}

function AddLiElementsPage() {
    let previousPage = currentPage - visiblePages;
    let nextPage = currentPage + visiblePages;



    CurrentPage(currentPage);
    PreviousPages(previousPage);
    NextPages(nextPage);
}

function CurrentPage(currentPage) {
    ulPage.append(`<li class="page-item active" id="${currentPage}"><span class="page-link">${currentPage}</span></li>`)
}

function PreviousPages(previousPage) {
    for (previousPage; previousPage <= currentPage - 1; previousPage++) {
        if (previousPage >= 1) {
            ulPage.find('li.page-item.active').before(`<li class="page-item" id="${previousPage}"><a class="page-link" href="#${previousPage}" onclick ="ChangePage(${previousPage}, RecordPerPages())">${previousPage}</a></li>`);
        }

    }
}

function NextPages(nextPage) {
    for (nextPage; nextPage >= currentPage + 1; nextPage--) {
        if (nextPage <= Number($(totalPagesString).val())) {
            ulPage.find('li.page-item.active').after(`<li class="page-item" id="${nextPage}"><a class="page-link" href="#${nextPage}" onclick ="ChangePage(${nextPage}, RecordPerPages())">${nextPage}</a></li>`);
        }
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
            $('div#loading').removeClass('d-flex')
            $('div#loading').addClass('d-none')
            $('div#ElementsList').html(partialView)
            AddLiElementsPage()
        },
        error: function (code) {
            console.log(code.status)
        }
    })
}

