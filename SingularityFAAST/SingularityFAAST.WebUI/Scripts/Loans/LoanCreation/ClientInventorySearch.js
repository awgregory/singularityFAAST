//region notes
//disclaimer: learning curve ahead. especially with javascript and all the crazy
//things you can do. I'll try to explain as I go.

//here we run an IIFE, (immediately invoked function expression)
//it provides an anonymous closure so variables inside shouldn't collide with oher scripts

//IIFE example: (function(){ alert('iife') }());

//javascript easily lets you do this kind of weird thing
//we define an anonymous function with that parameter '$', that is called by the
//last line '(jQuery)'.

//see http://www.adequatelygood.com/JavaScript-Module-Pattern-In-Depth.html
//for more information

//endregion
(function ($) {
    /*
     so inside here, we can define functions and variables
     that will essentially be static/private. name variables without worry!

     so clientSearch will exist on the page and everything inside it will have
     run and works, but it is essentially sealed from public access
     runs a function that hits the server searching for a client by a parameter
     */
    //region ajax identifiers for use inside functions
    var clientSearchButton = $('#clientSearchButton');
    var clientSearchInput = $('#clientSearchInput');
    // var clientIdHiddenFormInput = $('#clientIdHidden');

    var itemSearchButton = $('#itemSearchButton');


    //endregion

    //region wire up event handlers

    //search buttons
    clientSearchButton.on('click', function () {
        getFakeClientsWithOneParameter(clientSearchInput.val())
    });


    //endregion

    //region functions!
    function updateClientIdFormValue(e) {
        //tied to checkboxes - grabs the click event, inspects its data set which we defined as 'data-client-id'
        var chosenClientId = e.target.dataset.clientId; //even if custom data attribute contains hyphens, this uses camel case

        if (!chosenClientId) //if variable doesn't mean anything: we are invalid/error
            return; //safely exit doing nothing

        console.log('client id chosen: ' + chosenClientId);
        //set the hidden form input's value to the clientId extracted from the click event
        $('#clientIdHidden').val(chosenClientId)
    }

    function getFakeClientsWithOneParameter(sendThis) {
        var pathToControllerMethod = "/Loan/SearchFakeClients/";
        var methodArguments = "?searchString=" + sendThis;

        $.ajax({
            url: pathToControllerMethod + methodArguments,
            success: function (result) {
                /*
                 instead of writing a whole load of messy javascript in here
                 we have a nice simple function call, passing along the result
                 */
                buildFakeClientTable(result);
            },
            error: function (error) {
                console.log(error)
            }
        })
    }

    function buildFakeClientTable(results) {
        /*
         lot of ways to handle table creation, I'll use an example I know works,
         but is admittedly not the cleanest/nicest way
         */

        //jQuery reference of table
        var table = $('#clientSearchTable');
        //step 1: clear the table - regardless of content, a new search brings new results
        table.find("tr:gt(0)").remove();

        //step 2: check if there's any info at all to build a table out of
        if (results.length > 0) {
            //build a row for each result - string gore ahead
            for (var i = 0; i < results.length; i++) {
                table.append(
                    '<tr>' +
                    '<td>' +
                        '<input type="radio" name="radioClientId" data-client-id="' + results[i].ClientID + '"' +
                        '</input>' +
                    '</td>' + //select box -- we will have jquery grab each checkbox created, and put a function on it
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

        //once table is built, do we have valid markup to attach to -- assign the functions here!
        $("input:radio[name=radioClientId]").on('click', updateClientIdFormValue);
    }

    //endregion
}(window.jQuery));
