$(document).ready(function () {
  $('#addAddressButton').on('click', function () {
    $('#addressList').addClass('d-none');
    $('#addressForm').removeClass('d-none');
  });

  $('#cancelAddAddress').on('click', function () {
    $('#addressForm').addClass('d-none');
    $('#addressList').removeClass('d-none');
  });

  $('#addPaymentButton').on('click', function () {
    $('#paymentList').addClass('d-none');
    $('#paymentForm').removeClass('d-none');
  });

  $('#cancelAddPayment').on('click', function () {
    $('#paymentForm').addClass('d-none');
    $('#paymentList').removeClass('d-none');
  });
});
