//region notes
//disclaimer: learning curve ahead. especially with javascript and all the crazy
//things you can do. I'll try to explain as I go.

//we create a variable that will live on the window object. if you remember from
//internet programming, the window object is the top most global object you can
//write javascript against. in this way, we can namespace some of our functionality.

//here we declare clientSearch to be the expression result of an anonymous function
//javascript easily lets you do this kind of weird thing
//we define an anonymous function with that parameter '$', that is called by the
//last line '(jQuery)'.

//we can do this because jQuery exists at the window level thanks to the scaffolded
//MVC project defaults. you can check for youself: launch the app, hit F12 on chrome,
//in the Console, begin typing jQuery. if it can auto-complete to it, it's there.
//endregion
(function ($) {
    /*
        so inside here, we can define functions and variables
        that will essentially be static/private. name variables without worry!

        so clientSearch will exist on the page and everything inside it will have
        run and works, but it is essentially sealed from public access
        runs a function that hits the server searching for a client by a parameter
    */

    //waits until DOM is loaded and all mark up ready
    $().ready(function() {
        //region ajax identifiers for use inside functions
        var clientSearchButton = $('#clientSearchButton');
        var clientSearchInput = $('#clientSearchInput');

        var itemSearchButton = $('#itemSearchButton');


        //endregion
        //region wire up event handlers
        clientSearchButton.on('click', function() {
            // alert(clientSearchInput.val());
            getFakeClientsWithOneParameter(clientSearchInput.val())
        });

        //endregion

        //region functions!
        function getFakeClientsWithOneParameter(sendThis){
            var pathToControllerMethod = "/Loan/SearchFakeClients/";
            var methodArguments = "?searchString=" + sendThis;

            /* what?
             scope. if we want to pass scope and function accessibility around,
             this is a way of handling that.
             */

            $.ajax({
                url: pathToControllerMethod + methodArguments,
                success: function(result) {
                    /*
                     instead of writing a whole load of messy javascript in here
                     we have a nice simple function call, passing along the result
                     */
                    buildFakeClientTable(result);
                },
                error: function(error) {
                    console.log(error)
                }
            })
        }

        function buildFakeClientTable(results) {
            /*
             lot of ways to handle table creation, I'll use an example I know works,
             but is admittedly not the cleanest/nicest looking way
             */

            //jQuery reference of table
            var table = $('#clientSearchTable');
            //step 1: clear the table - regardless of content, a new search brings new results
            table.find("tr:gt(0)").remove();
            //jquery syntax to find by id (defined in the cshtml), and remove all rows

            //step 2: check if there's any info at all to build a table out of
            if (results.length > 0) {
                //build a row for each result - string gore ahead
                for (var i = 0; i < results.length; i++){
                    table.append(
                        '<tr>' +
                        '<td>' + 'select box' + '</td>' + //select box
                        '<td>' + results[i].ClientID + '</td>' + //client id
                        '<td>' + results[i].FirstName + '</td>' + //first name
                        '<td>' + 'Last Name' + '</td>' + //last name
                        '<td>' + 'home phone' + '</td>' + //home phone
                        '<td>' + 'email' + '</td>' + //email
                        '<td>' + 'eligibilty' + '</td>' + //eligibility -- will need a ToString() version
                        '</tr>'
                    )
                }
            }
        }

        //endregion
    });
}(window.jQuery));