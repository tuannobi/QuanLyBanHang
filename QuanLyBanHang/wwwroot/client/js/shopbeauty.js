$(document).ready(function () {

  $(".colection ul li a").click(function () {
    var id_menu = $(this).attr("itemprop");
    loc = 'all';
    locgia = 'all';
    $(this).attr('href', 'category.php?id=' + id_menu + '&sortby=default' + '&loc=' + loc + '&locgia=' + locgia + '&search=' + 'all');
  });

  $(".product-image").hover(function () {
    $(".quick_action", this).toggle();
  });

  //$(".product-title a").click(function () {
  //  var image_id = $(this).attr("itemprop");
  //  $(this).attr('href', 'product.php?id=' + image_id);
  //});

/*  $(".btn-quicklook").click(function () {
    var image_id = $(this).attr("itemprop");
    $(this).attr('href', 'product.php?id=' + image_id);
  });*/


});
