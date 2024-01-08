// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function PopRentalsMiniDash() {
    fetch('/api/Dashboard/Rentals').then(response => {
        if (!response.ok)
            throw new Error('Network response was not ok');
        return response.json();
    }).then(data => {
        console.log(data);
        $('#totalRentals').text(data.totalRentals);
        $('#totalUsedCoupons').text(data.couponsUsed);
        $('#pendingReturns').text(data.pendingReturns);
        $('#overdueReturns').text(data.overDueReturns);

    }).catch(error => {
        toastr.error('Error Occured');
    });
}

function PopCustomersMiniDash() {
    fetch('/api/Dashboard/Customers').then(response => {
        if (!response.ok)
            throw new Error('Network response was not ok');
        return response.json();
    }).then(data => {
        console.log(data);
        $('#custCount').text(data.customerCount);
        $('#subCustCount').text(data.subscribedCount);
        $('#custWithMembership').text(data.withMemberships);
        $('#custWoutMembership').text(data.withoutMemberships);
    }).catch(error => {
        toastr.error('Error Occured');
    });
}