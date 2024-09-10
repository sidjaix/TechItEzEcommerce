$(document).ready(function () {
  $('#addAddressButton').on('click', function () {
    toggleAddressForm(true); // Show the form and hide the list
  });

  $('.btnEditAddress').on('click', function () {
    var action = $(this).data('url');
    var url = decodeURIComponent(action);
    getAddressToEdit(url);
  });

  $('#btnCancelAddress').on('click', function () {
    resetAddressForm();
    toggleAddressForm(false); // hide the form and show the list
  });

  $('.btnOrderDetails').on('click', function () {
    var encodedUrl = $(this).data('url');
    var url = decodeURIComponent(encodedUrl);
    getOrderDetails(url);
  });
});

//  function to get adddress and bind data to edit Address Form
function getAddressToEdit(url) {
  $.ajax({
    url: url,
    method: 'GET',
    success: function (data) {
      // Populate the form with the fetched address data
      $('#AddressId').val(data.addressId);
      $('#FirstName').val(data.firstName);
      $('#LastName').val(data.lastName);
      $('#UnitNumber').val(data.unitNumber);
      $('#AreaOrStreet').val(data.areaOrStreet);
      $('#TownOrCity').val(data.townOrCity);
      $('#Landmark').val(data.landmark);
      $('#State').val(data.state);
      $('#Pincode').val(data.pincode);

      // Set the isDefaultAddress checkbox
      $('#IsDefaultAddress').prop('checked', data.isDefaultAddress);
      toggleAddressForm(true); // Show the form and hide the list
    },
  });
}

//  Function to get order details
function getOrderDetails(url) {
  $.ajax({
    url: url,
    method: 'GET',
    success: function (data) {
      $('#orderDetailsContent').html(data);
      toggleOrderDetails(true); // Show the Order details and hide the Orders content
    },
  });
}

// Function to reset address form fields
function resetAddressForm() {
  $('#AddressId').val(0);
  $('#FirstName').val('');
  $('#Lastname').val('');
  $('#UnitNumber').val('');
  $('#AreaOrStreet').val('');
  $('#TownOrCity').val('');
  $('#Landmark').val('');
  $('#State').val('');
  $('#Pincode').val('');
  // Set the isDefaultAddress checkbox
  $('#IsDefaultAddress').prop('checked', false);
}

// Function to toggle the address form and list visibility
function toggleAddressForm(showForm) {
  if (showForm) {
    $('#addressList').addClass('d-none');
    $('#addressForm').removeClass('d-none');
  } else {
    $('#addressList').removeClass('d-none');
    $('#addressForm').addClass('d-none');
  }
}

// Function to toggle the order details and orders list visibility
function toggleOrderDetails(showOrderDetails) {
  if (showOrderDetails) {
    $('#userOrdersContent').addClass('d-none');
    $('#orderDetailsContent').removeClass('d-none');
  } else {
    $('#userOrdersContent').removeClass('d-none');
    $('#orderDetailsContent').addClass('d-none');
  }
}
