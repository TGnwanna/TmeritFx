// LOGIN POST ACTION
function loginPost() {
    debugger;
    var data = {};
    data.Email = $("#Email").val();
    data.Password = $("#Passwrd").val();
    let loginViewModel = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Home/Login',
        data:
        {
            loginViewModel: loginViewModel
        },
        success: function (result) {
            debugger;
            if (!result.isError)
            {
                successAlertWithRedirect(result.msg, result.dashboard)
            }
            else
            {
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            errorAlert(ex);
        }
    });
    
}
// LOGIN POST ACTION//

// Admin Registration POST ACTION
function RegisterAdmin(){
    debugger;
    var data = {};
    data.FirstName = $('#FirstName').val();
    data.LastName = $('#LastName').val();
    data.Email = $('#Email').val();
    data.UserName = $('#UserName').val();
    data.Password = $('#Passwrd').val();
    data.ConfirmPassword = $('#Cpasswrd').val();
    let applicationViewModel = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Accounts/CreateAdminAccount',
        data:
        {
            applicationUserViewModel: applicationViewModel,
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                var url = '/Home/Index';
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
// Admin Registration POST ACTION//

// REGULAR EMPLOYEE Registration POST ACTION
function RegisterEmployee() {
    debugger;
    var data = {};
    data.FirstName = $('#FirstName').val();
    data.LastName = $('#LastName').val();
    data.EmployeeId = $('#EmployeeId').val();
    data.UserName = $('#UserName').val();
    data.Email = $('#Email').val();
    data.DepartmentId = $('#DepartmentId').val();
    data.BranchId = $('#BranchId').val();
    data.Password = $('#Passwrd').val();
    data.ConfirmPassword = $('#Cpasswrd').val();
    let staffDataCollected = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Accounts/CreateEmployeeAccount',
        data:
        {
            employeeRawData: staffDataCollected,
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
            }
            else
            {
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            errorAlert(ex);
        }
    });
}
// Admin Registration POST ACTION//


// GET EMPLOYEE FOR EDIT
function editEmployee(Id) {
    debugger;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Accounts/GetEmployeeAccountForEditOrDelete', // we are calling json method
        data: { Id: Id },
        success: function (data) {
            debugger;
            if (!data.isError) {
                $('#EditFirstName').val(data.firstName);
                $('#EditLastName').val(data.lastName);
                $('#EditEmployeeId').val(data.employeeId);
                $('#EditUserName').val(data.userName);
                $('#EditEmail').val(data.email);
                $('#EditDepartmentId').val(data.departmentId);
                $('#EditBranchId').val(data.branchId);
            }
        }
    });
};
// GET EMPLOYEE FOR EDIT//

// CREATE BRANCH POST ACTION
function CreateBranch() {
    debugger;
    var data = {};
    data.Name = $("#Branch").val();
    let newBranchModel = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/CreateBranch',
        data:
        {
            newBranch: newBranchModel
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
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
// CREATE BRANCH POST ACTION

//GET BRANCH FOR EDIT
function editBranch(Id){
    debugger;
    var data = {};
    data.IntID = Id;
    data.actionType = "BranchGet";
    let collectedData = JSON.stringify(data);
    debugger;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Admin/GetBranchAndDepartmentViewById', // we are calling json method
        data:
        {
            getDetails: collectedData
        },
        success: function (data){
            debugger;
            if (!data.isError){
                $("#editBranchId").val(data.id);
                $("#EditBranchName").val(data.name);
            }
        }
    });
};
//GET BRANCH FOR EDIT//

// UPDATE BRANCH POST ACTION
function EditBranchPost() {
    debugger;
    var data = {};
    data.id = $("#editBranchId").val();
    data.name = $("#EditBranchName").val();
    let branchToEdit = JSON.stringify(data);
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/EditBranchPost',
        data:
        {
            branchToEdit: branchToEdit
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
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
// UPDATE BRANCH POST ACTION

//GET BRANCH FOR DELETE
function deleteBranch(Id) {
    debugger;
    var data = {};
    data.IntID = Id;
    data.actionType = "BranchGet";
    let collectedData = JSON.stringify(data);
    debugger;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Admin/GetBranchAndDepartmentViewById', // we are calling json method
        data:
        {
            getDetails: collectedData
        },
        success: function (data) {
            debugger;

            if (!data.isError) {
                $("#deleteBranchId").val(data.id);
            }
        }
    });
};
//GET BRANCH FOR DELETE//

// DELETE BRANCH POST ACTION
function DeleteBranchPost() {
    debugger;
    var data = {};
    data.Id = $("#deleteBranchId").val();
    let branchToDeleted = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/DeleteBranchPost',
        data:
        {
            branchToDeleted: branchToDeleted
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
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
// DELETE BRANCH POST ACTION

// CREATE DEPARTMENT POST ACTION
function CreateDepartment() {
    debugger;
    var data = {};
    data.Name = $("#Department").val();
    let newDepartmentModel = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/CreateDepartment',
        data:
        {
            newDepartment: newDepartmentModel
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
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
// CREATE DEPARTMENT POST ACTION

//GET DEPARTMENT FOR EDIT
function editDepartment(Id) {
    debugger;
    var data = {};
    data.IntID = Id;
    data.actionType = "DepartmentGet";
    let collectedData = JSON.stringify(data);
    debugger;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Admin/GetBranchAndDepartmentViewById', // we are calling json method
        data:
        {
            getDetails: collectedData
        },
        success: function (data) {
            debugger;

            if (!data.isError) {
                $("#editDepartmentId").val(data.id);
                $("#EditDepartmentName").val(data.name);
            }
        }
    });
};
//GET DEPARTMENT FOR EDIT//

// UPDATE DEPARTMENT POST ACTION
function EditDepartmentPost() {
    debugger;
    var data = {};
    data.Id = $("#editDepartmentId").val();
    data.Name = $("#EditDepartmentName").val();
    let departmentToEdit = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/EditDepartmentPost',
        data:
        {
            departmentToEdit: departmentToEdit
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
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
// UPDATE DEPARTMENT POST ACTION

//GET DEPARTMENT FOR EDIT
function deleteDepartment(Id) {
    debugger;
    var data = {};
    data.IntID = Id;
    data.actionType = "DepartmentGet";
    let collectedData = JSON.stringify(data);
    debugger;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '/Admin/GetBranchAndDepartmentViewById', // we are calling json method
        data:
        {
            getDetails: collectedData
        },
        success: function (data) {
            debugger;

            if (!data.isError) {
                $("#deleteDepartmentId").val(data.id);
            }
        }
    });
};
//GET DEPARTMENT FOR EDIT//

// DELETE DEPARTMENT POST ACTION
function DeleteDepartmentPost() {
    debugger;
    var data = {};
    data.Id = $("#deleteDepartmentId").val();
    let departmentToDeleted = data;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/DeleteDepartmentPost',
        data:
        {
            departmentToDelete: departmentToDeleted
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
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
// DELETE DEPARTMENT POST ACTION

// CHANGE PASSWORD POST ACTION
function ChangePasswordPost() {
    debugger;
    var data = {};
    data.OldPassword = $("#OldPasswrd").val();
    data.NewPassword = $("#NewPasswrd").val();
    data.ConfirmPassword = $("#Cpasswrd").val();
    let changePasswordDetails = data;
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
// CHANGE PASSWORD POST ACTION