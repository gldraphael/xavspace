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

console.log('initialized');

$('#search').typeahead(
    {
        highlight: true
    },
    {
        name: 'boards',
        displayKey: 'value',
        source: boards.ttAdapter(),
        templates: {
            header: '<h4>Notice Boards</h4>'
        }
    },
    {
        name: 'Notices',
        displayKey: 'value',
        source: notices.ttAdapter(),
        templates: {
            header: '<h4>Notices</h4>'
        }
    }
);
