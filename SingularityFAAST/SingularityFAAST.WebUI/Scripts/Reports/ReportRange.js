(function ($) {
    //on document loaded
    $().ready(function () {
        var errorText = $('#error-text');

        errorText.hide();

        //on button click, do the function defined here
        $('#submitLoanReport').on('click', function (e) {
            //grab date values
            var startDate = $('#startDate').val();
            var endDate = $('#endDate').val();

            //date objects to compare - created from above values
            var start = new Date(startDate).getTime();
            var end = new Date(endDate).getTime();

            if (true) { //replace this with the proper logic
                console.log('condition hit')
                errorText.show();
                e.preventDefault(); //e is the event originating from submit button click
                //if we hit the error, prevent submission
            }
        })
    });
})(window.jQuery);
