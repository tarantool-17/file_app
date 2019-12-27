$(document).ready(function(){
    
    var tree;
    
    $.ajax({
        dataType: 'json',
        url: 'tree',
        success: function(data) {
            console.log(data);

            tree = mapNode(data);
            console.log(tree);

            buildTree(tree);
        }
    });
    
    $('#deleteButton').click(
        function(){
            alert();
        }
    );

    $('#copyButton').click(
        function(){
            var sel = $('#tree').treeview(true).getSelected()[0];
            $('#tree').treeview(true).remove(sel);
        }
    );

    $('#renameButton').click(
        function(){
            var selected = $('#tree').treeview(true).getSelected()[0];
            
            var item = findItem(tree, selected.type, selected.id);
            
            item.text = "selected";

            buildTree(tree);
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

    function buildTree(data){
        $('#tree').treeview({
            data: [data],
            showBorder: false,
            onNodeSelected: function(event, node) {
                var size = calculateSize(node);
                updateInfo(size);
            },
        });
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
        $('#size').text("Selected size: " + size);
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