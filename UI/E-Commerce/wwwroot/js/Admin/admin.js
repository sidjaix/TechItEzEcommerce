$(document).ready(function () {
  // Initialize pagination for a specific table
  applyPagination('#products', 10); // Example for a table with ID 'userTable'
});

function applyPagination(tableId, rowsPerPage) {
  var $table = $(tableId);
  var $rows = $table.find('tbody tr');
  var rowsCount = $rows.length;
  var pageCount = Math.ceil(rowsCount / rowsPerPage);
  var $pagination = $(tableId + ' + div');
  var currentPage = 0; // Track the current page index

  // Generate pagination controls
  function generatePaginationControls() {
    $pagination.empty();
    // $pagination.append(
    //   '<li class="page-item disabled"><span class="page-link">Previous</span></li>'
    // );

    for (var i = 1; i <= pageCount; i++) {
      $pagination.append('<a href="#" class="page-item">' + i + '</a>');
    }
    $pagination.append('<a href="#" class="page-item"> Next</a>');
  }

  // Display rows for the current page
  function displayRows() {
    $rows.hide();
    var start = currentPage * rowsPerPage;
    var end = start + rowsPerPage;
    $rows.slice(start, end).show();
  }

  // Handle pagination click
  function handlePaginationClick(e) {
    e.preventDefault();
    var $target = $(e.target);
    var pageIndex;

    // if ($target.text() === 'Previous') {
    //   pageIndex = currentPage - 1;
    // } else
    if ($target.text() === 'Next') {
      pageIndex = currentPage + 1;
    } else {
      pageIndex = parseInt($target.text()) - 1;
    }

    if (pageIndex < 0) pageIndex = 0;
    if (pageIndex >= pageCount) pageIndex = pageCount - 1;

    if (pageIndex !== currentPage) {
      currentPage = pageIndex;
      displayRows();
      updatePaginationControls();
    }
  }

  // Update the state of the pagination controls
  function updatePaginationControls() {
    $pagination.find('li').removeClass('active');
    $pagination
      .find('li')
      .eq(currentPage + 1)
      .addClass('active'); // +1 for "Previous" button

    $pagination.find('.page-item').removeClass('disabled');
    // if (currentPage === 0) {
    //   $pagination.find('.page-item:contains("Previous")').addClass('disabled');
    // }
    if (currentPage === pageCount - 1) {
      $pagination.find('.page-item:contains("Next")').addClass('disabled');
    }
  }

  // Initialize pagination
  generatePaginationControls();
  displayRows();
  updatePaginationControls();

  // Bind click event for pagination controls
  $pagination.on('click', 'a', handlePaginationClick);
}
