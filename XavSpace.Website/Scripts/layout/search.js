var boards = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    remote: '/Search/Boards?q=%QUERY'
});

var notices = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    remote: '/Search/Notices?q=%QUERY'
});

boards.initialize();
notices.initialize();

$('#search').typeahead(
    {
        highlight: true
    },
    {
        name: 'boards',
        displayKey: 'value',
        source: boards.ttAdapter(),
        templates: {
            header: '<h3 class="search-type">Notice Boards</h3>'
        }
    },
    {
        name: 'Notices',
        displayKey: 'value',
        source: notices.ttAdapter(),
        templates: {
            header: '<h3 class="search-type">Notices</h3>'
        }
    }
);
