$(document).ready(function () {
  var toastElList = $('.toast');
  toastElList.each(function () {
    var toast = new bootstrap.Toast($(this));
    toast.show();
  });

  // Define the progress percentages for each stage
  const stages = {
    pending: 0,
    shipped: 50,
    delivered: 100,
  };

  // Function to animate progress and change stage colors
  function animateProgress(stage) {
    $('#progress-bar')
      .css('width', stages[stage] + '%')
      .attr('aria-valuenow', stages[stage]);

    if (stage === 'pending') {
      $('#pending').removeClass('bg-secondary').addClass('bg-primary');
      $('#shipped').removeClass('bg-primary').addClass('bg-secondary');
      $('#delivered').removeClass('bg-primary').addClass('bg-secondary');
    } else if (stage === 'shipped') {
      $('#pending').removeClass('bg-primary').addClass('bg-secondary');
      $('#shipped').removeClass('bg-secondary').addClass('bg-primary');
      $('#delivered').removeClass('bg-primary').addClass('bg-secondary');
    } else if (stage === 'delivered') {
      $('#pending').removeClass('bg-primary').addClass('bg-secondary');
      $('#shipped').removeClass('bg-primary').addClass('bg-secondary');
      $('#delivered').removeClass('bg-secondary').addClass('bg-primary');
    }
  }

  // Simulate the progress stages (can be triggered by an event in real use)
  setTimeout(() => animateProgress('pending'), 500); // Start with Pending
  setTimeout(() => animateProgress('shipped'), 3000); // Move to Shipped
  setTimeout(() => animateProgress('delivered'), 6000); // Move to Delivered
});
