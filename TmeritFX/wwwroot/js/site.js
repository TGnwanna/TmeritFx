// Investors Registration
// Registration for page 3 Button Submt
function AccountsRegistraion(RefID) {
    debugger
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var cpassword = document.getElementById("password_confirm").value;
    var policy = document.querySelector("checkbox3");
    var terms = document.querySelector("#checkbox2");
    var PVPCheck = document.querySelector("#checkbox3Validation");
    var termsCheck = document.querySelector("#checkbox2Validation");

    debugger
    if (name != "" && email != "" && password != "" && cpassword != "" && password == cpassword && policy.checked == true && terms.checked == true) {
        debugger;
        MainRegistrationScript(RefID);
    }
    else {

        if (name == "") {
            document.getElementById("nameValidation").style.display = "block";
        } else {
            document.getElementById("nameValidation").style.display = "none";
        }
        if (email == "") {
            document.getElementById("emailValidation").style.display = "block";
        } else {
            document.getElementById("emailValidation").style.display = "none";
        }
        if (password == "") {
            document.getElementById("passwordValidation").style.display = "block";
        } else {
            document.getElementById("passwordValidation").style.display = "none";
            if (password != cpassword) {
                document.getElementById("confirm_passwordValidation").style.display = "block";
            } else {
                document.getElementById("confirm_passwordValidation").style.display = "none";
            }
        }
        if (policy.checked == false) {
            PVPCheck.style.display = "block";
        } else {
            PVPCheck.style.display = "none";
        }
        if (terms.checked == false) {
            termsCheck.style.display = "block";
        } else {
            termsCheck.style.display = "none";
        }
    }

}

// APPLICATION REQUEST 
function MainRegistrationScript(RefID) {
    debugger;
    var data = {};
    data.Name = $('#name').val();
    data.Email = $('#email').val();
    data.Password = $('#password').val();
    data.ConfirmPassword = $('#password_confirm').val();
    data.RefID = RefID;

    let userViewModel = JSON.stringify(data);
    debugger;
    if (userViewModel != "")
    {
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/Accounts/Registeration',
            data:
            {
                userRegistrationData: userViewModel,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Accounts/Login';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                errorAlert("Error occured try again");
            }
        });
    }
    else {
        errorAlert("Incorrect Details");
    }

}

// LOGIN POST ACTION
function loginPost() {
    debugger;
    var data = {};
    data.Email = $("#email").val();
    data.Password = $("#password").val();
    if (data.Email != "" && data.Password != "") {
        let loginViewModel = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Accounts/Login',
            data:
            {
                loginData: loginViewModel
            },
            success: function (result) {
                debugger;
                if (result.isNotVerified) {
                    errorAlert(result.msg)
                }
                else if (result.isDeactivated) {
                    errorAlert(result.msg)
                }
                else if (!result.isError) {
                    successAlertWithRedirect(result.msg, result.dashboard)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else
    {
        if (data.Email == "")
        {
            document.querySelector("#emailValidation").style.display = "block"
        } else {
            document.querySelector("#emailValidation").style.display = "none"
        }
        if (data.Password == "") {
            document.querySelector("#passwordValidation").style.display = "block"
        } else {
            document.querySelector("#passwordValidation").style.display = "none"
        }
    }
}

// CHANGE PASSWORD POST ACTION
function ChangePasswordPost() {
    debugger;
    var data = {};
    data.OldPassword = $("#OldPasswrd").val();
    data.NewPassword = $("#NewPasswrd").val();
    data.ConfirmPassword = $("#Cpasswrd").val();
    if (data.OldPassword != "" && data.NewPassword != "" && data.NewPassword == data.ConfirmPassword) {
        let changePasswordDetails = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Accounts/ChangePasswordPost',
            data:
            {
                userPasswordDetails: changePasswordDetails
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    successAlertWithRedirect(result.msg, result.url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else {
        if (data.OldPassword == "") {
            document.getElementById("oldPassVDT").style.display = "block";
        } else {
            document.getElementById("oldPassVDT").style.display = "none";
        }
        if (data.NewPassword == "") {
            document.getElementById("newPassVDT").style.display = "block";
        } else {
            document.getElementById("newPassVDT").style.display = "none";
            if (data.Password != data.ConfirmPassword) {
                document.getElementById("cnewPassVDT").style.display = "block";
            } else {
                document.getElementById("cnewPassVDT").style.display = "none";
            }
        }
    }

}

//<!----------ADMIN SCRIPTS STARTED-------------------------->
function ActivateAndDeactivateClientPost(action, id) {
    debugger;
    var data = {};
    data.Id = id;
    data.ActionType = action;
    let collectedData = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        dataType: 'json',
        url: '/Admin/ClientTurnOnAndOff',
        data:
        {
            collectedClientData: collectedData,
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                var url = '/Admin/Clients';
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("Error occured try again");
        }
    });
}
//<!----------ADMIN SCRIPTS ENDED-------------------------->

//<!----------INVESTOR SCRIPTS STARTED-------------------------->
function InvestmentPost(action, Id) {
    debugger
    var data = {};
    if (action == "CREATE") {
        data.ActionType = action;
        var amt = $('#amount').val();
        data.Amount = parseFloat(amt).toFixed(4);
        var amtValidation = document.querySelector("#amountValidation");
        data.HashID = $('#hash').val();
        var hashIDValidation = document.querySelector("#hashValidation");
        data.Plan = $('#plan_name').val();
        debugger;
        if (data.Amount >= 100 && data.HashID != "") {
            debugger;
            SendInvestmentData2Kontrola(data);
        }
        else
        {
            debugger;
            if (data.Amount < 100) {
                amtValidation.style.display = "block";
            } else {

                amtValidation.style.display = "none";
            }
            if (data.HashID == "") {
                hashIDValidation.style.display = "block";
            } else {

                hashIDValidation.style.display = "none";
            }
        }
    }
    if (action == "ACTIVATE") {
        data.ActionType = action;
        data.Id = Id;
        if (Id != null) {
            debugger;
            SendInvestmentData2Kontrola(data);
        }
    }
}

function SendInvestmentData2Kontrola(data){
    debugger;
    let investmentViewModel = JSON.stringify(data);
    if (investmentViewModel != "")
    {
        $.ajax({
            type: 'Post',
            dataType: 'json',
            url: '/Investor/InvestmentPost',
            data:
            {
                investmentData: investmentViewModel,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Investor/Plans';
                    successAlertWithRedirect(result.msg, url)
                }
                else
                {
                    errorAlert(result.msg)
                }
            },
            error: function (ex) {
                errorAlert("Error occured try again");
            }
        });
    }
    else
    {
        errorAlert("Incorrect Details");
    }
}

function PlaceWithdrawal() {
    debugger;
    var data = {};
    data.Amount = $("#amount").val();
    data.Address = $("#address").val();
    var totalWithdrawableBal = $("#chk").val();
    if (data.Amount != "" && data.Address != "" && data.Amount <= totalWithdrawableBal) {
        let order = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            dataType: 'Json',
            url: '/Investor/WithdrawalPost',
            data:
            {
                withdrawalData: order
            },
            success: function (result) {
                debugger;
                if (result.isNotVerified) {
                    errorAlert(result.msg)
                }
                else if (result.isDeactivated) {
                    errorAlert(result.msg)
                }
                else if (!result.isError) {
                    var url = '/Investor/Withdrawal';
                    successAlertWithRedirect(result.msg, url)
                }
                else {
                    errorAlert(result.msg)
                }
            },
            Error: function (ex) {
                errorAlert(ex);
            }
        });
    }
    else {
        if (data.Amount == "") {
            document.querySelector("#amountVDT").style.display = "block"
        } else {
            document.querySelector("#amountVDT").style.display = "none"
            if (data.Amount > totalWithdrawableBal) {
                errorAlert("Amount entered exceeded your available balance")
            }
        }
        if (data.Address == "") {
            document.querySelector("#addressVDT").style.display = "block"
        } else {
            document.querySelector("#addressVDT").style.display = "none"
        }
    }
}

//COPY TEXT
function Copyddress(addressType) {
    debugger;
    var id = "";
    if (addressType == "BTC") {
        id = "btc"
    }
    if (addressType == "USDT") {
        id = "usdt"
    }
    if (addressType == "RefID") {
        id = "refID"
    }
    var copiedAddress = document.getElementById(id);

    // Select the text field
    copiedAddress.select();
    copiedAddress.setSelectionRange(0, 99999); // For mobile devices

    // Copy the text inside the text field
    navigator.clipboard.writeText(copiedAddress.value);

    // Alert the copied text
    if (copiedAddress.value) {
        successAlert("Copied: " + copiedAddress.value)
    }
    /*alert("Copied: " + copiedAddress.value);*/
}
//<!----------INVESTOR SCRIPTS ENDED-------------------------->