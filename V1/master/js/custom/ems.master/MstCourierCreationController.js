(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierCreationController', MstCourierCreationController);

    MstCourierCreationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$filter', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstCourierCreationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $filter, DownloaddocumentService, cmnfunctionService) {
        $scope.title = 'MstCourierCreationController';
        var vm = this;
        vm.title = 'MstCourierCreationController';

        $scope.courier_type = $location.search().courier_type;

        activate();
        function activate() {
            $scope.rdbcustomer = 'Yes';
            $scope.customerno = false;
            $scope.customeryes = true;

          /*  $scope.courier_type = localStorage.getItem('courier_type');*/

            if ($scope.courier_type == "Physical Inward" || $scope.courier_type == "Physical Outward") {
                document.getElementById("txtcourier_company_name").disabled = false;
                $scope.txtcourier_company_name = "";
                $scope.txtpod_no = "";
                $scope.isDisabled = true;

            }
            else {
                $scope.isDisabled = false;
            }


            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1

            };

            var url = 'api/MstCourierManagement/GetCadCustomerName';
            SocketService.get(url).then(function (resp) {
                $scope.cadcustomer_list = resp.data.cadcustomer_list;
            });

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/MstCourierManagement/GetCourierCompany';
            SocketService.get(url).then(function (resp) {
                $scope.couriercompany_list = resp.data.couriercompany_list;
            });
           
            var url = 'api/MstCourierManagement/GetCourierDocTempClear';
            SocketService.get(url).then(function (resp) {
            });

            $scope.rdbcustomer_yes = function () {
                $scope.customerno = false;
                $scope.customeryes = true;
            }
            $scope.rdbcustomer_no = function () {
                $scope.customerno = true;
                $scope.customeryes = false;
            }

        }

        $scope.courierback = function () {
            if ($scope.courier_type == 'Courier Inward') {
                $location.url('app/MstCourierMgmtsummary?lstab=CI');
            }
            else if ($scope.courier_type == 'Courier Outward') {
                $location.url('app/MstCourierMgmtsummary?lstab=CO');
            }
            else if ($scope.courier_type == 'Physical Outward') {
                $location.url('app/MstCourierMgmtsummary?lstab=PO');
            }
            else if ($scope.courier_type == 'Physical Inward') {
                $location.url('app/MstCourierMgmtsummary?lstab=PI');
            }
            else {
                $location.url('app/MstCourierMgmtsummary?lstab=CI');
            }

        }

        $scope.OnChangeCourierType = function (val) {
            if (val == "Physical Inward" || val == "Physical Outward") {
                $scope.txtcourier_company_name = "";
                $scope.txtpod_no = "";
                $scope.isDisabled = true;
            }
            else {
                $scope.isDisabled = false;
            }
        }

        $scope.onselectedchangecustomer = function (application_gid) {
            $scope.application_gid = localStorage.setItem('onchangecustomer_gid', application_gid);
            var params = {
                application_gid: $scope.cbocustomergid.application_gid
            }
            var url = 'api/MstCourierManagement/Getcustomer2sanction';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno_list = resp.data.sanctionrefno_list;
            });
        }

        $scope.deletedocument = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Uploaded Document?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var params = {
                        courierdocument_gid: val
                    }
                    var url = 'api/MstCourierManagement/DeleteCourierDoc';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            SweetAlert.swal('Document Deleted Successfully!');

                            var url = 'api/MstCourierManagement/GetCourierDoc';
                            SocketService.get(url).then(function (resp) {
                                $scope.commondocument = resp.data.courierdocument_list;
                            });
                        }
                        else {
                            unlockUI();
                            SweetAlert.swal('Error Occured');

                        }

                    });
                }

            });

        }
        $scope.downloadsdocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.doc_downloads = function (val1, val2) {           
                DownloaddocumentService.Downloaddocument(val1, val2);           
        }

        $scope.commondocumentupload = function (val) {
            var frm = new FormData();
            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };

                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);

                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    return false;
                }

            }
            frm.append('document_title', $scope.txtdocument_title);           

            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstCourierManagement/CourierDocUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#file").val('');
                    $scope.txtdocument_title = '';
                    var url = 'api/MstCourierManagement/GetCourierDoc';
                    SocketService.get(url).then(function (resp) {
                        $scope.commondocument = resp.data.courierdocument_list;
                    });
                    $scope.uploadfrm = undefined;

                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtdocument_title = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
            
        }
        
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.commondocument.length; i++) {                
                    DownloaddocumentService.Downloaddocument($scope.commondocument[i].document_path, $scope.commondocument[i].document_name);
             }
        }

        $scope.courierSubmit = function () {
            var courier_company_name;
            if ($scope.courier_type == "Courier Inward" || $scope.courier_type == "Courier Outward") {
                if ($scope.txtcourier_company_name == undefined || $scope.txtcourier_company_name == "") {
                    Notify.alert('Select the Courier Company', 'warning');
                    return;
                }
                else {
                    $scope.courier_company_name = $scope.txtcourier_company_name.couriercompany_name;
                    $scope.courier_company_gid = $scope.txtcourier_company_name.couriercompany_gid;
                }

                if ($scope.txtpod_no == undefined || $scope.txtpod_no == "") {
                    Notify.alert('Enter the POD No.', 'warning');
                    return;
                }
            }
            else {
                courier_company_name = "";
                $scope.txtpod_no = "";
            }
            if ($scope.cbocustomer2sanction_gid == undefined) {
                $scope.sanctionref_no = '';
                $scope.sanction_gid = '';
            }
            else {
                $scope.sanctionref_no = $scope.cbocustomer2sanction_gid.sanction_refno;
                $scope.sanction_gid = $scope.cbocustomer2sanction_gid.application2sanction_gid;
            }
            //if ($scope.rdbcustomer == "No") {
            //    $scope.customer_name = $scope.txtcustomer_name;
            //    $scope.customer_gid = '';
            //}
            //else {
                $scope.customer_name = $scope.cbocustomergid.customer_name;
                $scope.customer_gid = $scope.cbocustomergid.application_gid;
            /*}*/
            var params = {
                customer_name: $scope.customer_name,
                customer_gid: $scope.customer_gid,
                sanctionref_no: $scope.sanctionref_no,
                sanction_gid: $scope.sanction_gid,
                MdlCourierByList: $scope.txtcourier_sender_name,
                MdlCourierToList: $scope.txtcourier_handoverto,
                document_type: $scope.txtdocument_type,
                date_of_courier: $scope.txtdate_of_courier,
                pod_no: $scope.txtpod_no,
                couriercompany_gid: $scope.courier_company_gid,
                couriercompany_name: $scope.courier_company_name,
                address: $scope.txtaddress1,
                courier_type: $scope.courier_type,
                remarks: $scope.txtremarks
            }           
            var url = 'api/MstCourierManagement/SubmitCourierDtl';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();                   
                    if ($scope.courier_type == 'Courier Inward') {
                        $location.url('app/MstCourierMgmtsummary?lstab=CI');
                    }
                    else if ($scope.courier_type == 'Courier Outward') {
                        $location.url('app/MstCourierMgmtsummary?lstab=CO');
                    }
                    else if ($scope.courier_type == 'Physical Outward') {
                        $location.url('app/MstCourierMgmtsummary?lstab=PO');
                    }
                    else if ($scope.courier_type == 'Physical Inward') {
                        $location.url('app/MstCourierMgmtsummary?lstab=PI');
                    }                  
                    Notify.alert(resp.data.message, 'success');
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }
                
            });
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
    }
})();