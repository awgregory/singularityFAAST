﻿//region notes
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
    $().ready(function () {


        /*
         so inside here, we can define functions and variables
         that will essentially be static/private. name variables without worry!

         so clientInventorySearch will exist on the page and everything inside it will have
         run and works, but it is essentially sealed from public access
         runs a function that hits the server searching for a client by a parameter
         */

        //region ajax identifiers for use inside functions
        var clientSearchButton = $('#clientSearchButton');
        var clientSearchInput = $('#clientSearchInput');
        // var clientIdHiddenFormInput = $('#clientIdHidden');

        var itemSearchButton = $('#itemSearchButton');
        var itemSearchInput = $('#itemSearchInput');

        var itemRemoveButton = $('#itemRemoveButton');
        var itemRemoveInput = $('#itemRemoveInput');

        var chooseNewClientButton = $('#chooseNewClient');

        //endregion

        //region - globals(ish)
        var itemIds = itemIds || [];
        //endregion

        //region wire up event handlers

        //search buttons
        clientSearchButton.on('click', function () {
            getFakeClientsWithOneParameter(clientSearchInput.val())
            //set error message to blank
            $('#clientInfoAlert').text('')
        });
        itemSearchButton.on('click', function () {
            getInventoryWithOneParameter(itemSearchInput.val())
            //set error message to blank
            $('#itemsInfoAlert').text('')
        });

        itemRemoveButton.on('click', function () {
            removeItemFromLoan(itemRemoveInput.val())
            //set error message to blank
        });

        chooseNewClientButton.on('click', function () {
            $("#clientSearchButton").prop('disabled', false);
            $("#clientSearchInput").prop('disabled', false);
            $('#cName').text('');
        });


        //$("input:radio[name=purp]").on('click', updatecheckboxesThreeCategories);
        //$("input:radio[name=ppda]").on('click', updatecheckboxesThreeCategories);
        //endregion


        //region functions!
        function updateClientIdFormValue(e) {
            //tied to checkboxes - grabs the click event, inspects its data set which we defined as 'data-client-id'
            var chosenClientId = e.target.dataset.clientId; //even if custom data attribute contains hyphens, this uses camel case
            //var clientName = chosenClientId1.replace(/[0-9]/g);  //removes the digits
            //var chosenClientId = chosenClientId1.replace(/\D/g, '');  //removes the alphas

            //this was removed to allow for different client to be chosen
            //if ($('#clientIdHidden').val().trim().length > 0) {
            //    console.log('one client already entered')
            //    $('#clientInfoAlert').text('Only one client can be chosen.')  //' Please remove first client by using the Remove Selected Client button.')  //this will never need to happen. Must reload page for different client
            //    chosenClientId = null
            //}

            if (!chosenClientId) //if variable doesn't mean anything: we are invalid/error
                return; //safely exit doing nothing

            console.log('client id chosen: ' + chosenClientId);
            //console.log('client id chosen: ' + clientName);
            //set the hidden form input's value to the clientId extracted from the click event
            $('#clientIdHidden').val(chosenClientId);

            //Update the info panel
            var infoPanel = $('#cName');
            infoPanel.append('<h5>Client ' + chosenClientId + ' is borrowing items: </h5>');
        }


        function updateInventoryIdFormValue(e) {
            //tied to checkboxes - grabs the click event, inspects its data set which we defined as 'data-client-id'
            var chosenInventoryId = e.target.dataset.inventoryId; //even if custom data attribute contains hyphens, this uses camel case
            //var invName = chosenInventoryId.replace(/[0-9]/g);  //removes the digits won't work for this purpose if Amazon "3"g
            //chosenInventoryId = chosenInventoryId.replace(/\D/g, '');  //removes the alphas

            //if ($('#inventoryItemIdsHidden').val().trim().length > 0) {   //incorrect; this is if there is a maximum number of items per loan
            //    console.log('maximum number of items chosen')
            //    $('#itemsInfoAlert').text('Only 10 items allowed per loan.')   //check this
            //    chosenInventoryId = null
            //}

            if (!chosenInventoryId) //if variable doesn't mean anything: we are invalid/error
                return; //safely exit doing nothing
            //Update the info panel

            if (itemIds) {
                for (var i = 0; i < itemIds.length; i++) {
                    if (chosenInventoryId == itemIds[i]) {  //if item being added is the same as an item in the itemIds array (would've been added below)...

                        //$('#itemsInfoAlert').text('Item can be added only once.')
                        //$('#iItems').text('');  //emptys entire div

                        //remove from itemids array
                        itemIds.splice($.inArray(chosenInventoryId, itemIds), 1);

                        //remove from infoPanel the div with the item id
                        //$("#iItems .chosenInventoryId h5").remove();

                        //var elem = document.getElementById(".chosenInventoryId");
                        //elem.remove();
                        //console.log(elem)

                        $(".chosenInventoryId").remove();


                        //make null
                        chosenInventoryId = null;
                        return;
                    }
                }
            }

            console.log('item id chosen: ' + chosenInventoryId);

            //set the hidden form input's value to the inventoryId extracted from the click event
            //we need to be appending to a array, then, just before form submission
            //appoint that array to the form value
            itemIds.push(chosenInventoryId); //TODO: create separate function that handles this logic
            console.log(itemIds);

            ////Update the info panel
            var infoPanel = $('#iItems');
            infoPanel.append('<div class = "' + chosenInventoryId + '"><h5>Item #' + chosenInventoryId + '</h5></div>');
        }


        function updatecheckboxesThreeCategories() {
            //get the checkbox values
            var purposeDropdown = $('input[name=purp]:checked').val();
            var purposeTypeDropdown = $('input[name=ppda]:checked').val();
            if ((purposeDropdown || purposeTypeDropdown) == null) {
                $('#purposeInfoAlert').text("Both Purpose and Purpose Type are required.")
                return;
            }
            $('#purposeHidden').val(purposeDropdown);
            $('#purposeTypeHidden').val(purposeTypeDropdown);
            console.log(purposeDropdown);
            console.log(purposeTypeDropdown);

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

        function getInventoryWithOneParameter(sendThis) {
            var pathToControllerMethod = "/Loan/SearchInventory/";
            var methodArguments = "?searchString=" + sendThis;

            $.ajax({
                url: pathToControllerMethod + methodArguments,
                success: function (result) {
                    buildInventoryTable(result);
                },
                error: function (error) {
                    console.log(error)
                }
            })
        }

        function removeItemFromLoan(sendThis) {
            var pathToControllerMethod = "/Loan/DeleteIteminEdit/";  //change to CancelItem
            var methodArguments = "?searchString=" + sendThis;

            $.ajax({
                url: pathToControllerMethod + methodArguments,
                success: function () {
                    return;
                },
                error: function (error) {
                    console.log(error)
                }
            })
        }


        //function removeItemFromLoanTable - click on x takes to here, that loads fake client table


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
                for (var i = 0; i < results.length; i++) {    //was   i < results.length   -if less than 11, only shows first 11 if input left blank- what if there are more than 11 with same name?
                    table.append(
                        '<tr>' +
                        //'<td>' +
                        '<td>' + '<input type="button" class="btn btn-primary form-control" name="addCButton" value="Add" data-client-id="' + results[i].ClientID + '"' + '</input>' + '</td>' + //add
                        //'<input type="radio" name="radioClientId" data-client-id="' + results[i].ClientID + '"' +  //+results[i].FirstName + ' ' + results[i].LastName + '"' +
                        //'</input>' +
                        //'</td>' + //select box -- we will have jquery grab each checkbox created, and put a function on it
                        '<td>' + results[i].ClientID + '</td>' + //client id
                        '<td>' + results[i].FirstName + '</td>' + //first name
                        '<td>' + results[i].LastName + '</td>' + //last name
                        '<td>' + results[i].CellPhone + '</td>' + //home phone
                        '<td>' + results[i].Email + '</td>' + //email
                        '<td>' + results[i].LoanEligibilty + '</td>' + //eligibility -- will need a ToString() version
                        '</tr>'
                    )
                }
            } else {
                $('#clientInfoAlert').text('No Client found with that last name.')
            }
            //once table is built, do we have valid markup to attach to -- assign the functions here!
            //$("input:radio[name=radioClientId]").on('click', updateClientIdFormValue);

            $("input:button[name=addCButton]").on('click', updateClientIdFormValue);
            $("input:button[name=addCButton]").on('click',
                function () {
                    $(this).css('background', 'green');
                    $("input:button[name=addCButton]").prop('disabled', true);
                    $("#clientSearchButton").prop('disabled', true);
                    $("#clientSearchInput").prop('disabled', true);
                    $("#chooseNewClient").prop('disabled', false);
                });
            $("input:button[name=addCButton]").on('click', function () {
                $(this).val('Added')
            });

        }

        function buildInventoryTable(results) {
            //jQuery reference of table
            var table = $('#inventorySearchTable');
            //step 1: clear the table - regardless of content, a new search brings new results
            table.find("tr:gt(0)").remove();

            //step 2: check if there's any info at all to build a table out of
            if (results.length > 0) {
                //build a row for each result - string gore ahead
                for (var i = 0; i < results.length; i++) {        //results.length
                    table.append(
                        '<tr>' +
                        //'<td>' +
                        '<td>' + '<input type="button" class="btn btn-primary form-control" name="addInvButton" value="Add" data-inventory-id="' + results[i].InventoryItemId + '"' + '</input>' + '</td>' + //add
                        //'<input type="radio" name="radioInventoryId" data-inventory-id="' + results[i].InventoryItemId + '"' +  //results[i].ItemName + '"' +   //checkbox
                        //'</input>' +
                        //'</td>' + //select box -- we will have jquery grab each checkbox created, and put a function on it
                        '<td>' + results[i].InventoryItemId + '</td>' + //Inventory id
                        '<td>' + results[i].ItemName + '</td>' + //Item name
                        '<td>' + results[i].Manufacturer + '</td>' + //manufacturer
                        '<td>' + results[i].Description + '</td>' + //description
                        '<td>' + results[i].Accesories + '</td>' + //accessories
                        '<td>' + results[i].Availability + '</td>' + //availability
                        '<td>' + results[i].Damages + '</td>' + //damages
                        '</tr>'
                    )
                }
            } else {
                $('#itemsInfoAlert').text('No items with that name exist in inventory.')
            }

            //once table is built, do we have valid markup to attach to -- assign the functions here!
            //$("input:radio[name=radioInventoryId]").on('click', updateInventoryIdFormValue);   //radio displays only if there are results   //checkbox


            $("input:button[name=addInvButton]").on('click', updateInventoryIdFormValue);
            $("input:button[name=addInvButton]").on('click', function () {
                $(this).toggleClass("btn-primary btn-danger")
            });
            $("input:button[name=addInvButton]").on('click', function () {
                $(this).val(function (i, v) {
                    return v === 'Add' ? 'Remove' : 'Add'
                })
            });

        }

        $("form").submit(function (e) {
            if (!isLoanValid()) {
                e.preventDefault();
                return;
            }

            if (itemIds) {
                //var hiddenInput = $("input[name='InventoryItemIds']");
                //hiddenInput.val(itemIds);
                for (var i = 0; i < itemIds.length; i++) {
                    $('<input />')
                        .attr('type', 'hidden')
                        .attr('name', 'InventoryItemIds[' + i + ']')
                        .attr('value', itemIds[i])
                        .appendTo("form");
                }
            }
            updatecheckboxesThreeCategories()
        });

        function isLoanValid() {
           var valid = true;
           $('#clientErrorTextDiv').hide();
           $('#itemErrorTextDiv').hide();

           if (!$('#clientIdHidden').val()) {
               $('#clientErrorTextDiv').show();
               valid = false;
           }

           if (itemIds.length === 0) {
               $('#itemErrorTextDiv').show();
               valid = false;
           }

           return valid;
        }
    })

    //endregion
}(window.jQuery));

