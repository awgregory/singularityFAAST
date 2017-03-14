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
var clientSearch = (function ($) {
    //so inside here, we can define functions and variables
    //that will essentially be static/private. name variables without worry!

    //so clientSearch will exist on the page and everything inside it will have
    //run and works, but it is essentially sealed from public access
    var myVariable = 'transient data';

    //alerts a result of a method, supplying the parameter
    alert(alertMeOfMyVariable('transient parameter'));

    //runs a function that hits the server searching for a client by a parameter
    getFakeClientsWithOneParameter('toof');

    //dummy function
    function alertMeOfMyVariable(param){
        return (myVariable + ' and some parameter: ' + param);
    }

    //define the server hit
    function getFakeClientsWithOneParameter(sendThis){
        //just a cleaner way of defining a string variable
        var pathToControllerMethod = "/Loan/SearchFakeClients/";
        var methodArguments = "?searchString=" + sendThis;

        $.ajax({
            url: pathToControllerMethod + methodArguments,
            success: function(result) {
                //success, got something back from the server, MAY be empty, may not
                alert("check the console and open object up");
                console.log(result);
            },
            error: function(error) {
                //control automatically handed to error function if some xhr error
                //at the very least log to console to see, 'swallowed' exceptions are bad
                //especially for developers that follow you
                console.log(error)
            }
        })
    }
}(window.jQuery));