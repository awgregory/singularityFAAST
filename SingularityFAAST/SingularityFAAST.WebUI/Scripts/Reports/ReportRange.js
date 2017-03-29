//(function ($) {
//    //on document loaded
//    $().ready(function () {
//        //on button click, do the function defined here
//        $('#submitLoanReport').on('click', function () {
//            //grab date values
//            var startDate = $('#startDate').val();
//            var endDate = $('#endDate').val();

//            //date objects to compare - created from above values
//            var start = new Date(startDate).getTime();
//            var end = new Date(endDate).getTime();

//            if (start > end) { //error
//                alert('ERROR')
//                console.log('error error')
//            }
//        })
//    });
//})(window.jQuery);

(function () {
    window.jQuery(document).ready(function ($) {
        console.log($)
    });

})(window.jQuery);