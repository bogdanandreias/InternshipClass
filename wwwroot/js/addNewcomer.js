﻿  
function addNewcomer() {
           
            var node = document.createElement("li");
            node.innerText = newcomer.value;
            list.appendChild(node);
 }

document.getElementById("add").onclick = function () { addNewcomer() };