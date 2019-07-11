$(function () {
    $("#accordion").accordion({ collapsible: true, heightStyle: "content" });
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

var spellTypeDropDown = document.getElementById("spellType");
spellTypeDropDown.onchange = function () {
    var castingDuration = document.getElementById("levelBlock");
    castingDuration.style.display = this.value === "2" || this.value === "3" ? "none" : "block";
};

var actionTypeDropDown = document.getElementById("actionType");
actionTypeDropDown.onchange = function () {
    var castingDuration = document.getElementById("castingDuration");
    var trigger = document.getElementById("triggerBlock");
    castingDuration.style.display = this.value === "3" ? "block" : "none";
    trigger.style.display = this.value === "1" || this.value === "2" ? "block" : "none";
};

var durationTypeDropDown = document.getElementById("durationType");
durationTypeDropDown.onchange = function () {
    var castingDuration = document.getElementById("durationBlock");
    castingDuration.style.display = this.value === "" | this.value === "0" || this.value === "1" ? "none" : "block";
};
