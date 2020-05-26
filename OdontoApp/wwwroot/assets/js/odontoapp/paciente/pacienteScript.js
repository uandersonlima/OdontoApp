let start,
    search,
    end,
    numberPage,
    recordPerPage,
    totalPages,
    totalRecords,
    currentPage,
    visiblePages=5,
    urlPag;


function ChangePage(changedPage, recordPerpage){
    $('li.page-item.active').removeClass('active');
    $(`li.page-item#${changedPage}`).addClass('active');
}

function RecordPerPages(){
    return Number(recordPerPage.value);
}

function AddLiElementsPage() {
    let previousPage = currentPage - visiblePages;
    let nextPage = currentPage + visiblePages;
    PreviousPages(previousPage);
    NextPages(nextPage);
}
function PreviousPages(previousPage) {
    for (previousPage; previousPage <= currentPage - 1; previousPage++) {
        console.log(previousPage)
        if (previousPage >= 1) {
            console.log(previousPage)
            $('ul.pagination li.page-item.active').before(`<li class="page-item" id="${previousPage}"><a class="page-link" href="#${previousPage}" onclick = ChangePage(${previousPage}, ${ RecordPerPages()})>${previousPage}</a></li>`);
        }
    }
}
function NextPages(nextPage) {
    for (nextPage; nextPage >= currentPage + 1; nextPage--) {
        if (nextPage <= totalPages) {
            console.log(nextPage)
            $('ul.pagination li.page-item.active').after(`<li class="page-item" id="${nextPage}"><a class="page-link" href="#${nextPage}" onclick = ChangePage(${nextPage}, ${RecordPerPages()})>${nextPage}</a></li>`);
        }
    }
}

function ConstructorAppView(searchArg = null, numberpageArg = null, recordperpageArg = null, startArg = null, endArg = null) {
    start = startArg;
    search = searchArg;
    end = endArg;
    numberPage = numberpageArg;
    recordPerPage = recordperpageArg;
}

function ConstructorUrl(url) {
    urlPag = url;
}

function AppViewModel() {
    let AppView = {
        Search: search,
        NumberPag: numberPage,
        RecordPerPage: recordPerPage,
        Start: start,
        End: end,
    }
    $.ajax({
        type: "GET",
        url: urlPag,
        async: true,
        data: AppView,
        contentType: 'application/json',
        success: function (partialView) {
            AddLiElementsPage();
            $('div#ElementsList').html(partialView);
        },
        error: function (code) {
            console.log(code.status)
        }
    })
}

