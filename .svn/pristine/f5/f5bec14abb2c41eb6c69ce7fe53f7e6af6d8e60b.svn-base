(function () {
    'use strict';

    angular
        .module('angle')
        .controller('newServiceTicketcontroller', newServiceTicketcontroller);

    newServiceTicketcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route'];

    function newServiceTicketcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'newServiceTicketcontroller';

        activate();

        function activate() {
            $scope.showval = false;
            $scope.hidecontact = false;
            $scope.hideapproval = false;

            var url = 'api/newServiceTicket/category';
            SocketService.get(url).then(function (resp) {


                $scope.category_list = resp.data.category_list;
            });

            $scope.close = function (val) {
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {

                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/newServiceTicket/tmpcleardocument';
            SocketService.get(url).then(function (resp) {
            });
        }


        // Get Sub_Category List //
        $scope.onselectedchange = function (category) {
            var params = {
                category_gid: category,
                employee_gid: $scope.employee
            };
            console.log(params);
            if ($scope.employee == "" && $scope.radio_selfothers=="") {
            }
            else if ($scope.radio_selfothers == "Self") {
                var url = 'api/newServiceTicket/employee_contactdetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.approvaldetails = resp.data;

                });
            }
            else {
                var url = 'api/newServiceTicket/employeecontactdetails';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.approvaldetails = resp.data;

                });

            }
            var url = 'api/newServiceTicket/subcategory';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.approval_flag == "Y") {
                    var modalInstance = $modal.open({
                        templateUrl: '/myModalContent.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {     

                        $scope.subcategory = resp.data;
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };     
                    }
                }
                    
                $scope.subcategory = resp.data;
                $scope.subcategory_list = resp.data.subcategory_list;
            });
        }

        // Get Type List //
        $scope.onselectedchangesubcategory = function (subcategory) {
            var params = {
                subcategory_gid: subcategory
            }
            var url = 'api/newServiceTicket/type';


            SocketService.getparams(url, params).then(function (resp) {

                $scope.type_list = resp.data.type_list;

            });
            
        }

        $scope.onselectedchangeemployee = function (employee) {
            var params = {
                employee_gid: employee,
                category_gid: $scope.category
            }
            var url = 'api/newServiceTicket/employeecontactdetails';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txtcontact_number = resp.data.employee_mobileno;
                    $scope.txtemail_address = resp.data.employee_emailid;
                    $scope.approvaldetails = resp.data;
                    $scope.hideapproval = true;
                }
                else {           
                    Notify.alert('You Cannot able to Raise Ticket to this Selected Employee', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                  
                }
            });
          
            $scope.hidecontact = true;

        }

        $scope.onchangeself = function (radio_selfothers) {
            var params = {
                category_gid: $scope.category
            }
            var url = 'api/newServiceTicket/employee_contactdetails';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txtcontact_number = resp.data.employee_mobileno;
                    $scope.txtemail_address = resp.data.employee_emailid;
                    $scope.approvaldetails = resp.data;        
                     
                }
                else {                    
                    Notify.alert('Contact Admin', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   
                }
             
            });
          

        }

        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.showval = true;
                $scope.hideval = true;               
                $scope.hidecontact = false;
                $scope.hideapproval = false;
                console.log("show");
                //$('#contactnumber').hide();
                $scope.txtcontact_number = "";
                $scope.txtemail_address = "";
                Notify.alert('Select Employee Name..!!', {
                    status: 'info',
                    pos: 'top-center',
                    timeout: 3000
                });
                
            }
                // SELF //
            else if (param == "hide") {
                $scope.showval = false;
                $scope.hideval = false;
                $scope.hidecontact = true;
                $scope.hideapproval = true;
                $scope.employee = "";
                $scope.txtcontact_number = "";
                $scope.txtemail_address = "";
            }
            else {
                $scope.showval = false;
                $scope.hideval = false;
            }
        }

        // Document Upload //
        $scope.upload = function (val, val1, name) {

            var item = {
                name: val[0].name,
                file: val[0]
            };
            
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "Default");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                    var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default");

            $scope.uploadfrm = frm;
            var url = 'api/newServiceTicket/UploadDocument';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                $("#addupload").val('');

                if (resp.data.status == true) {

                    Notify.alert('Document Uploaded Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        }

        // Document Delete //
        $scope.document_cancelclick = function (val, data) {
            var params = { id: val };
            var url = 'api/newServiceTicket/documentdelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.id == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.serviceticket_submit = function () {
            var params = {
                category_gid: $scope.category,
                subcategory_gid: $scope.subcategory,
                type_gid: $scope.type,
                raisedfor: $scope.radio_selfothers,
                raisedemployee: $scope.employee,
                txtcontact_number: $scope.txtcontact_number,
                txtemail_address: $scope.txtemail_address,
                txt_title: $scope.txt_title,
                txt_remarks: $scope.txt_remarks,
                file_name: $scope.modelhere

            }

            var url = 'api/newServiceTicket/raiseticket';
         lockUI();
            SocketService.post('api/newServiceTicket/raiseticket', params).then(function (resp) {
           unlockUI();
                if (resp.data.status == true) {
                    $state.go('app.itDashboard');
                    Notify.alert('Ticket Raised Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert('Contact Admin', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        $scope.back = function () {
            $state.go('app.itDashboard');
        }

    }
})();
