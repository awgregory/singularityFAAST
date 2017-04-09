(function ($) {
    var itemSearchButton = $('#itemSearchButton');
    var itemSearchInput = $('#itemSearchInput');

    var itemRemoveButton = $('#itemRemoveButton');
    var itemRemoveInput = $('#itemRemoveInput');

    //endregion

    //region - globals(ish)
    var itemIds = itemIds || [];
    //endregion


    itemSearchButton.on('click',
        function () {
            getInventoryWithOneParameter(itemSearchInput.val())
            //set error message to blank
            $('#itemsInfoAlert').text('')
        });

    itemRemoveButton.on('click',
        function() {
            removeItemFromLoan(itemRemoveInput.val())
            //set error message to blank
        });


    function updateInventoryIdFormValue(e) {
        //tied to checkboxes - grabs the click event, inspects its data set which we defined as 'data-client-id'
        var chosenInventoryId = e.target.dataset
            .inventoryId; //even if custom data attribute contains hyphens, this uses camel case
        //var invName = chosenInventoryId.replace(/[0-9]/g);  //removes the digits won't work for this purpose if Amazon "3"g
        //chosenInventoryId = chosenInventoryId.replace(/\D/g, '');  //removes the alphas

        //if ($('#inventoryItemIdsHidden').val().trim().length > 0) {   //incorrect; this is if there is a maximum number of items per loan
        //    console.log('maximum number of items chosen')
        //    $('#itemsInfoAlert').text('Only 10 items allowed per loan.')   //check this
        //    chosenInventoryId = null
        //}

        if (itemIds) {
            for (var i = 0; i < itemIds.length; i++) {
                if (chosenInventoryId == itemIds[i]) {
                    $('#itemsInfoAlert').text('Item can be added only once.')
                    return;
                }
            }
        }

        if (!chosenInventoryId) //if variable doesn't mean anything: we are invalid/error
            return; //safely exit doing nothing

        console.log('item id chosen: ' + chosenInventoryId);

        //set the hidden form input's value to the inventoryId extracted from the click event
        //we need to be appending to a array, then, just before form submission
        //appoint that array to the form value
        itemIds.push(chosenInventoryId); //TODO: create separate function that handles this logic
        console.log(itemIds);

        //Update the info panel
        var infoPanel = $('#iItems');
        infoPanel.append('<h5>Item #' + chosenInventoryId + '</h5>');
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
        var pathToControllerMethod = "/Loan/GetItemsListForEdit/";
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


    //function removeItemFromLoanTable - click on x takes to here, that loads fake client table like in other js script, using results like other method


    function buildInventoryTable(results) {
        //jQuery reference of table
        var table = $('#inventorySearchTable');
        //step 1: clear the table - regardless of content, a new search brings new results
        table.find("tr:gt(0)").remove();

        //step 2: check if there's any info at all to build a table out of
        if (results.length > 0) {
            //build a row for each result - string gore ahead
            for (var i = 0; i < results.length; i++) { //results.length 
                table.append(
                    '<tr>' +
                    //'<td>' +
                    '<td>' +
                    '<input type="button" class="btn btn-primary" name="addInvButton" value="Add" data-inventory-id="' +
                    results[i].InventoryItemId +
                    '"' +
                    '</input>' +
                    '</td>' + //add
                    //'<input type="radio" name="radioInventoryId" data-inventory-id="' + results[i].InventoryItemId + '"' +  //results[i].ItemName + '"' +   //checkbox
                    //'</input>' +
                    //'</td>' + //select box -- we will have jquery grab each checkbox created, and put a function on it
                    '<td>' +
                    results[i].InventoryItemId +
                    '</td>' + //Inventory id
                    '<td>' +
                    results[i].ItemName +
                    '</td>' + //Item name
                    '<td>' +
                    results[i].Manufacturer +
                    '</td>' + //manufacturer
                    '<td>' +
                    results[i].Description +
                    '</td>' + //description
                    '<td>' +
                    results[i].Accesories +
                    '</td>' + //accessories
                    '<td>' +
                    results[i].Availability +
                    '</td>' + //availability 
                    '<td>' +
                    results[i].Damages +
                    '</td>' + //damages 
                    '</tr>'
                )
            }
        } else {
            $('#itemsInfoAlert').text('No items with that name exist in inventory.')
        }
        //once table is built, do we have valid markup to attach to -- assign the functions here!
        //$("input:radio[name=radioInventoryId]").on('click', updateInventoryIdFormValue);   //radio displays only if there are results   //checkbox

        $("input:button[name=addInvButton]").on('click', updateInventoryIdFormValue);
        $("input:button[name=adInvButton]").on('click', function() { $(this).css('background', 'green') });
        $("input:button[name=addInvButton]").on('click', function () { $(this).val('Added') });

        //Submit - add inventoryItemIds before submission
        $("form")
            .submit(function (e) {
                //validate inventoryId array
                //debugger;
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
            })
    }
}(window.jQuery));