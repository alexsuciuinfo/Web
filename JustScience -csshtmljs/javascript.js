window.onload = MyMain;

function MyMain(){
	
	if(location.pathname.substring(location.pathname.lastIndexOf("/") + 1)=="Marigenii.html") 
	httpRequest("./poze.json", poze);
	document.getElementById("b").onclick = conectare;
	if(document.getElementById("forma") != null)
		document.getElementById("forma").onsubmit = concurs;
	var x = Number(localStorage.getItem('conectat'));
	var y = Number(localStorage.getItem('conc'));
	if(x == 1) 
		isConnected();
	
	if(y == 1) { localStorage.setItem('conc',0); imagine_random("newtontoy.gif"); }
	if(y == 2) { localStorage.setItem('conc',0); imagine_random("einsteintoy.jpg");} 
	
	document.getElementById("adauga").onclick = adauga_cast1;
	
	document.getElementById("pozaein").onclick = muta_poza;

}


function adauga_cast(){
	
	var nume = document.getElementById("nume");
	var prenume = document.getElementById("prenume");
	var loc = document.getElementById("loc");
	var premiu = document.getElementById("premiu");
	var tabel = document.getElementById("tabel");
	var camp = document.createElement("tr");
	var tip1 = document.createElement("td");
	var tip2 = document.createElement("td");
	var tip3 = document.createElement("td");
	var textNode1 = document.createTextNode(nume.value + " " + prenume.value);
	var textNode2 = document.createTextNode(premiu.value);
	var textNode3 = document.createTextNode(loc.value);;
	tip1.appendChild(textNode3);
	tip2.appendChild(textNode1);
	tip3.appendChild(textNode2);
	camp.appendChild(tip1);
	camp.appendChild(tip2);
	camp.appendChild(tip3);
	tabel.appendChild(camp);
	
	
}

function adauga_cast1(){
	
	var nume = document.getElementById("nume");
	var prenume = document.getElementById("prenume");
	var loc = document.getElementById("loc");
	var premiu = document.getElementById("premiu");
	var tabel = document.getElementById("tabel");
	
	var camp = document.createElement("tr");
	tabel.appendChild(camp);
	
		var continut = [ 	
"<td>" + loc.value + "</td>",
"<td>" + nume.value + " " + prenume.value +"</td>",
"<td>" + premiu.value + "</td>"
].join("");

	camp.innerHTML = continut;	
	
}

function conectare(){
	
	var user = document.getElementById("user");
	var pass = document.getElementById("pass");
	if(user.value == "catalin" && pass.value == "12345")
	{
		isConnected();
		localStorage.setItem('conectat',1);
	}
	else alert("Invalid username or password");
	
}

function isConnected(){

		var bod = document.getElementsByClassName('backgr');
		var connect = document.createElement("p");
		connect.id = "id1";
		var node = document.createTextNode("Connected as catalin");
		connect.appendChild(node);
		var id = document.getElementById("id");
		var cont = document.createElement("input");
		cont.setAttribute("value","Log out");
		cont.setAttribute("type","button");
		cont.id = "logout";
		bod[0].replaceChild(connect,id);
		bod[0].appendChild(cont);
		document.getElementById("logout").onclick = restore;
		
}

function restore(){
	
	var continut = [ 
'<img src = "ScienceBanner.jpg" alt = "Foto banner stiinta" id = "poz_back"/>',
'<form id = "id">',
'<ul id = "ul">',
'<li> Username : <input type="text" required id = "user"> </li>',
'<li> Password : <input type="password" maxLength = "12" required id = "pass"> </li>',
'<li> <input type="button" value="Log in" id = "b"> </li>',
'</ul>',
'</form>'].join("");

	var forma = document.getElementsByClassName("backgr");
	forma[0].innerHTML = continut; 
	localStorage.setItem('conectat',0);
	document.getElementById("b").onclick = conectare;

}


function Random(element) {
	
	var x = document.body.offsetHeight-element.clientHeight;
	var y = document.body.offsetWidth-element.clientWidth;
	var X = Math.floor(Math.random()*x);
	var Y = Math.floor(Math.random()*y);
	return [X,Y];
}

function imagine_random(ob) {
	var img = document.createElement('img');
	img.setAttribute("style", "position:absolute;");
	img.setAttribute("src", ob);
	img.setAttribute("alt", "Aici este poza");
	img.setAttribute("width","7px");
	img.setAttribute("height","10px");
	img.id = "pozacastig1";
	document.body.appendChild(img);
	var poz = Random(img);
	img.style.top = poz[0] + 'px';
	img.style.left = poz[1] + 'px';
	document.getElementById("pozacastig1").onclick = castigator;
	
}

function muta_poza(){
	
	window.addEventListener('mousemove',muta,false);
}

function muta(e){
	
	var poza = document.getElementById("pozaein");
	var x = e.clientX;
	var y = e.clientY;
	poza.style.top = x + 'px';
	poza.style.left = y + 'px';
	
}

function castigator(){
	
	poza = document.getElementById("pozacastig1");
	poza.id = "pozacastig";
	alert("Ai castigat ! Vei primi informatii prin email");
	var v = document.getElementsByTagName('body');
	v[0].removeChild(poza);
	
}

function concurs() { 
	
	var elem = document.getElementById("forma").elements; 
	var alege = elem[0].elements;  
	
	var nr = Number(localStorage.getItem('conc'));
	
	//if(nr == 0) alert ("Ati participat deja la concurs");
	
	if(alege[6].checked) {alert("Pe ecran a aparut o figurina cu Newton ! Gaseste-o si e a ta !"); 
	localStorage.setItem('conc',1);
	}
	else if(alege[5].checked){
	alert("Pe ecran a aparut o figurina cu Einstein ! Gaseste-o si e a ta !"); 
	localStorage.setItem('conc',2);
	}
}

function httpRequest(url, callback) {
    var httpObj = false;
    
        httpObj = new XMLHttpRequest();
    
        if (!httpObj) return;

        httpObj.onreadystatechange = function() {
        if (httpObj.readyState == 4) {
          if (httpObj.status == 200) { callback(httpObj.responseText);}
          else {alert("eroare");}
        }
    };
    httpObj.open('GET', url, true);
    httpObj.send(null);
}




function poze(json) {
    var butoane = document.getElementsByClassName("butoane");
	var data = JSON.parse(json);
	var i;
	
	for(i=0; i<data.length; i++)
	{
		var but = document.createElement("input");
		but.setAttribute("value",i);
		but.setAttribute("type","button");
		but.className = "buton";
		//alert(butoane[Number(data[i].dom)]);
		butoane[Number(data[i].dom)-1].appendChild(but);
			
	}
	
	for(i=0; i<butoane.length; i++)
	butoane[i].addEventListener("click", arata_poza, false);
	
 
function arata_poza(e) {
    if (e.target !== e.currentTarget) {
        {
		var nr = e.target.value;
		var j = data[Number(nr)];
        var poza = document.getElementsByClassName("poz_gal1");
		poza[j.dom-1].setAttribute("src",j.src);
		poza[j.dom-1].setAttribute("alt",j.alt);
		var anc = document.getElementsByClassName("anc_gal");
		anc[j.dom-1].setAttribute("href",j.link);
		var textNode = document.createTextNode(j.alt);
		anc[j.dom-1].replaceChild(textNode,anc[j.dom-1].firstChild);
    }
    e.stopPropagation();
		
}

}}
