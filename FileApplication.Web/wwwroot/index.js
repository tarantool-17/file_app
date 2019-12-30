$(document).ready(function(){
    
    var tree;

    function getAndBuildTree() {
        
        $.ajax({
            dataType: 'json',
            url: 'tree',
            success: function (response) {
                console.log(response);

                tree = mapNode(response);

                console.log(response);

                buildTree(tree);
            }
        });
    }

    getAndBuildTree();

    $('.folderButton').hide();
    $('.fileButton').hide();
    
    function buildTree(data) {
        $('#tree').treeview({
            data: [data],
            showBorder: false,
            onNodeSelected: function(event, node) {

                toogleDisableButtons(node.id);
                toggleHideButton(node);
                
                var size = calculateSize(node);
                updateInfo(size);
            },
            onNodeUnselected: function(event, node) {

                toogleDisableButtons(null);
                
                updateInfo(0);
            },
        });
    }
    
    function toogleDisableButtons(id) {
        if (id) {
            $('#footer > input').removeAttr('disabled');
        } else {
            $('#footer > input').attr('disabled', true);
        }
    }
     
    function toggleHideButton(node){
        if(node.type == 1){
            $('.folderButton').hide();
            $('.fileButton').show();
        } else if(node.type == 2){
            $('.fileButton').hide();
            $('.folderButton').show();          
        }
    }
    
    $('#deleteButton').click(
        function(){
            var selected = $('#tree').treeview(true).getSelected()[0];

            var item = findItem(tree, selected.type, selected.id);

            var url = "component/" + selected.type + "/" + selected.id;
            
            $.ajax({
                type: 'delete',
                url: url,
                success: function(data) {
                    console.log(data);

                    getAndBuildTree();
                }
            });
        }
    );

    $('#copyButton').click(
        function(){
            var selected = $('#tree').treeview(true).getSelected()[0];

            var item = findItem(tree, selected.type, selected.id);

            $.ajax({
                type: 'put',
                url: "component/copy",
                data: JSON.stringify({
                    Type: item.type,
                    Id: item.id
                }),
                contentType: "application/json; charset=utf-8",
                success: function(data) {
                    console.log(data);

                    getAndBuildTree();
                }
            });
        }
    );

    $('#renameButton').click(
        function(){

            var selected = $('#tree').treeview(true).getSelected()[0];
            
            var item = findItem(tree, selected.type, selected.id);

            var newName = prompt("Please enter new name", item.text);

            if (newName && newName != item.text) {
                $.ajax({
                    type: 'put',
                    url: "component/rename",
                    data: JSON.stringify({
                        Type: item.type,
                        Id: item.id,
                        NewName: newName
                    }),
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
    
                        item.text = newName;
    
                        buildTree(tree);
                    }
                });               
            }          
        }
    );

    $('#createSubFolder').click(
        function(){
            var selected = $('#tree').treeview(true).getSelected()[0];

            var item = findItem(tree, selected.type, selected.id);

            var newName = prompt("Please enter sub folder name", "");

            if (newName) {
                $.ajax({
                    type: 'post',
                    url: 'folder',
                    data: JSON.stringify({
                        name: newName,
                        parentId: item.id
                    }),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        console.log(data);

                        getAndBuildTree();
                    }
                });
            }
        }
    );

    $('#upload').click(
        function(){
        }
    );

    $('#download').click(
        function(){
        }
    );
        
    function mapNode(node){
        var result = {
            text : node.name,
            id: node.id,
            type: node.type,
            size: node.size == undefined ? 0 : node.size
        };
        
        if(node.children) {
            
            result.nodes = new Array();
            
            node.children.forEach(x => {
                result.nodes.push(mapNode(x));
            });
        }
        
        return result;
    }    
    
    function calculateSize(node){
        var size = node.size;
        
        if(node.nodes){
            node.nodes.forEach(x => {
                size += calculateSize(x)
            });
        }
        
        return size;
    }
    
    function updateInfo(size){
        $('#size').text(size);
    }
    
    function findItem(node, type, id){
        
        if(node.id === id && node.type === type) {
            return node;
        }
        else if(node.nodes) {
            for(var i = 0; i < node.nodes.length; i++) {
                var item = findItem(node.nodes[i], type, id);
                
                if(item != null)
                    return item;
            }
        }

        return null;
    }
});