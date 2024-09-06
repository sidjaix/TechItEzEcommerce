$(document).ready(function () {
  $('.linkProductQuickView').on('click', function (e) {
    e.preventDefault();

    var url = decodeURIComponent($(this).data('action'));
    productQuickView(url);
  });
});

function productQuickView(url) {
  $.ajax({
    url: url,
    type: 'GET',
    contentType: 'application/json',
    //data: JSON.stringify(product),
    success: function (response) {
      var productQuickViewHolder = $('#productQuickView');
      productQuickViewHolder.html(response);
      productQuickViewHolder.find('.modal').modal('show');
    },
    error: function (xhr, status, error) {
      console.error('Error:', error);
    },
  });
}
