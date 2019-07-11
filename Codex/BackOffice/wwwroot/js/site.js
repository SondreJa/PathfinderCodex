$(function () {
    $("#accordion").accordion({ collapsible: true });
});

const tableConfig = {
    contentToolbar: ['tableRow', 'tableColumn', 'mergeTableCells']
};

ClassicEditor
    .create(document.querySelector('#editor'),
        {
            plugins: ['Table', 'TableToolbar', 'Bold', 'Italic', 'Essentials', 'List'],
            table: tableConfig,
            toolbar: ['bold', 'italic', 'bulletedList', 'insertTable', '|', 'undo', 'redo'],
            height:'800px'
        })
        .catch(error => {
    console.error(error);
});
