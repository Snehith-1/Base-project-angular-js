(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierEditController', MstCourierEditController);

    MstCourierEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService', 'SweetAlert', '$modal'];

    function MstCourierEditController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService, SweetAlert, $modal) {
        $scope.title = 'MstCourierEditController';
        var vm = this;
        vm.title = 'MstCourierEditController';

        $scope.courier_gid = $location.search().courier_gid;
        var courier_gid = $scope.courier_gid;
        $scope.page = $location.search().page;
        var page = $scope.page;

        activate();
        lockUI();
        function activate() {
           
            // Calender Popup... //
            $scope.courier_gid = courier_gid;
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

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ["dd-MM-yyyy"];
            vm.format = vm.formats[0];

            var url = 'api/MstCourierManagement/GetCourierDocTempClear';
            SocketService.get(url).then(function (resp) {
            });

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

            var params = {
                courier_gid: courier_gid
            }
            var url = 'api/MstCourierManagement/GetEditCourierDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    if (resp.data.customer_gid == "") {
                        $scope.rdbcustomeredit = "No";
                        $scope.customerno = true;
                        $scope.customernameedit = resp.data.customer_name;
                    }
                    else {
                        $scope.rdbcustomeredit = "Yes";
                        $scope.customeryes = true;
                        $scope.customername = resp.data.customer_gid;
                        var params = {
                            application_gid: resp.data.customer_gid
                        }
                        var url = 'api/MstCourierManagement/Getcustomer2sanction';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.sanctionrefno_list = resp.data.sanctionrefno_list;
                        });
                    }
                    
                    $scope.date_of_courier = resp.data.date_of_courier;
                    $scope.date_of_courier = Date.parse($scope.date_of_courier);                    
                    $scope.courierref_no = resp.data.courierref_no;
                    $scope.document_type = resp.data.document_type;
                    $scope.address = resp.data.address;
                    $scope.pod_no = resp.data.pod_no;
                    $scope.Cbocourier_company_name = resp.data.couriercompany_gid;
                   /* $scope.courier_company_name = resp.data.couriercompany_name;*/
                    $scope.courier_type = resp.data.courier_type;
                    $scope.remarks = resp.data.remarks;
                    $scope.courier_sender_name = resp.data.sender_gid;
                    $scope.handover_name = resp.data.courierhandover_to_gid;
                    $scope.uploadDoc_list = resp.data.uploadcourierdocument;
                    $scope.cbosanctionGid = resp.data.sanction_gid;
                    $scope.ack_status = resp.data.ack_status;
                    $scope.employee_list = resp.data.MdlEmployeeList;

                    if (resp.data.MdlCourierByList != null) {
                        $scope.sender_name = [];
                        var count = resp.data.MdlCourierByList.length;
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlCourierByList[i].employee_gid);
                            $scope.sender_name.push($scope.employee_list[indexs]);
                        }
                    }

                    if (resp.data.MdlCourierToList != null) {
                        var count = resp.data.MdlCourierToList.length;
                        $scope.handover_name = [];
                        for (var i = 0; i < count; i++) {
                            var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlCourierToList[i].employee_gid);
                            $scope.handover_name.push($scope.employee_list[indexs]);
                        }
                    }
                }

                if ($scope.courier_type == "Courier Outward" || $scope.courier_type == "Courier Inward") {
                    document.getElementById("courier_company_name").disabled = false;
                    document.getElementById("pod_no").disabled = false;
                    $scope.physical_value = false;
                }
                if ($scope.courier_type == "Courier Outward") {
                    $scope.courier_outward = true;
                    $scope.courier_inward = false;
                    $scope.physical_inward = false;
                    $scope.physical_outward = false;
                }

                if ($scope.courier_type == "Courier Inward") {
                    $scope.courier_outward = false;
                    $scope.courier_inward = true;
                    $scope.physical_inward = false;
                    $scope.physical_outward = false;
                }

                if ($scope.courier_type == "Physical Inward") {
                    $scope.courier_outward = false;
                    $scope.courier_inward = false;
                    $scope.physical_inward = true;
                    $scope.physical_outward = false;

                }
                if ($scope.courier_type == "Physical Outward") {
                    $scope.courier_outward = false;
                    $scope.courier_inward = false;
                    $scope.physical_inward = false;
                    $scope.physical_outward = true;

                }
                if ($scope.courier_type == "Physical Inward" || $scope.courier_type == "Physical Outward") {
                    document.getElementById("courier_company_name").disabled = true;
                    document.getElementById("pod_no").disabled = true;
                    $scope.physical_value = true;
                }

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

        $scope.onselectedchangecustomer = function (customername) {          
            var params = {
                application_gid: customername
            }
            var url = 'api/MstCourierManagement/Getcustomer2sanction';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno_list = resp.data.sanctionrefno_list;
            });
        }

        $scope.downloadsdocument = function (val1, val2) {
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

                    var params = {
                        courier_gid: $location.search().courier_gid
                    }
                    var url = 'api/MstCourierManagement/GetEditCourierDoc';
                    SocketService.getparams(url,params).then(function (resp) {
                        $scope.uploadDoc_list = resp.data.uploadcourierdocument;
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

        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.uploadDoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadDoc_list[i].document_path, $scope.uploadDoc_list[i].document_name);
            }
        }
       
        $scope.courierback = function () {
            $location.url('app/MstCourierMgmtsummary?lstab=' + page);
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

                            var params = {
                                courier_gid: $location.search().courier_gid
                            }
                            var url = 'api/MstCourierManagement/GetEditCourierDoc';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.uploadDoc_list = resp.data.uploadcourierdocument;
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

        $scope.update = function () {
            var courier_company_name;
            if ($scope.ack_status == 'Acknowledged') {
                if ($scope.ack_date == undefined || $scope.ack_date == "") {
                    Notify.alert('Select the Acknowledgement Date', 'warning');
                    return;
                }
            }
            if ($scope.courier_type == "Courier Inward" || $scope.courier_type == "Courier Outward") {
                if ($scope.Cbocourier_company_name == undefined || $scope.Cbocourier_company_name == "") {
                    Notify.alert('Select the Courier Company', 'warning');
                    return;
                }
                else {
                    courier_company_name = $('#courier_company_name :selected').text()
                }

                if ($scope.pod_no == undefined || $scope.pod_no == "") {
                    Notify.alert('Enter the POD No.', 'warning');
                    return;
                }
            }
            else {
                courier_company_name = "";
                $scope.pod_no = "";
            }
            if ($scope.date_of_courier == '' || $scope.date_of_courier == undefined || $scope.date_of_courier == null ||
                $scope.document_type == '' || $scope.document_type == undefined || $scope.document_type == null ||
                $scope.address == '' || $scope.address == undefined || $scope.address == null ||
                $scope.remarks == '' || $scope.remarks == undefined || $scope.remarks == null)
            {
                Notify.alert('Kindly, Select all Mandatory Fields', 'warning');
                return;

            }
            if ($scope.cbosanctionGid == (undefined || "")) {
                $scope.sanctionref_no = '';
                $scope.sanction_gid = '';
            }
            else {
                $scope.sanctionref_no = $('#sanctionGid :selected').text();
                $scope.sanction_gid = $scope.cbosanctionGid;
            }
            //if ($scope.rdbcustomeredit == "No") {
            //    var customername = $scope.customernameedit;
            //    $scope.customername = '';
            //}
           /* else {*/
                var customername = $('#customername :selected').text();
           /* }*/
            var params = {
                date_of_courier: $scope.date_of_courier,
                document_type: $scope.document_type,
                remarks: $scope.remarks,
                pod_no: $scope.pod_no,
                couriercompany_gid: $scope.Cbocourier_company_name,
                couriercompany_name: courier_company_name,
                address: $scope.address,
                customer_gid: $scope.customername,
                customer_name: customername,
                sanction_gid: $scope.sanction_gid,
                sanctionref_no: $scope.sanctionref_no,
                courierMgmt_gid: courier_gid,
                courier_type: $scope.courier_type,
                ack_status: $scope.ack_status,
                ack_date: $scope.ack_date,
                MdlCourierByList: $scope.sender_name,
                MdlCourierToList: $scope.handover_name
            }
            lockUI();
            var url = 'api/MstCourierManagement/PostUpdateCourier';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $location.url('app/MstCourierMgmtsummary?lstab=' + page);
                    Notify.alert(resp.data.message, 'success')
                }
                else {
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
