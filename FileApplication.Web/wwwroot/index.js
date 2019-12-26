$(document).ready(function(){
    
    $.ajax({
        dataType: 'json',
        url: 'tree',
        success: function(data) {
            console.log(data);

            $('#tree').fileTree({
                data: [data],
                sortable: false,
                selectable: true
            });
        }
    });
});