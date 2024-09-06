function addToCart(product, url) {
  $.ajax({
    url: url,
    type: 'POST',
    contentType: 'application/json',
    data: JSON.stringify(product),
    success: function (response) {
      console.log(response);
    },
    error: function (xhr, status, error) {
      console.error('Error:', error);
    },
  });
}
