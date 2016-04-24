$(document).ready(function(){
	
	setInterval(alternare,3000);

}) ;

var i=0;
var poze = new Array('poza1.jpg','poza2.jpg','ScienceBanner.jpg');

function alternare(){	
	
 $('#poz_back').fadeOut(4000, function()
  {
    $(this).attr('src', poze[i]);

    $(this).fadeIn(3000, function()
    {
      if (i == poze.length-1)
      {
        i = 0;
      }
      else
      {
        i++;
      }
    });
  });
}

	
