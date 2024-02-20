


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVisitorEditController', MstVisitorEditController);

    MstVisitorEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstVisitorEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVisitorEditController';
        $scope.visitor_gid = $location.search().visitor_gid;
        var visitor_gid = $scope.visitor_gid;
        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
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
           
            var today = new Date();

            var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();

            $scope.txtvisit_date = date;

            var url = 'api/MstVisitor/Branch';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branchname_list;
                unlockUI();
            });
            var params = {
                visitor_gid : $scope.visitor_gid
            }
           
           
            var url = 'api/MstVisitor/GetVisitorNameList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitorlist = resp.data.visitorname_list;
            });
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cbovisitingofficer_editlist = resp.data.employeelist;
                unlockUI();
            });

            var url = 'api/MstVisitor/GetVisitorView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtvisitorid = resp.data.visit_id;
                $scope.cbobranch_name = resp.data.branch_gid,
                $scope.txtvisitingofficialname = resp.data.visitingofficer_name,
                $scope.rbovisittype = resp.data.visiting_type,
                $scope.txtpurposeofvisit = resp.data.purpose_of_visit,
                $scope.txtvisit_date = resp.data.visit_date,         
                $scope.visitor_gid = resp.data.visitor_gid;

                if (resp.data.Tin_time == '0001-01-01T00:00:00') {
                    $scope.txtin_time = '';
                }
                else {
                    $scope.txtin_time = new Date(resp.data.Tin_time);
                   
                }
                if (resp.data.Ttentative_out_time == '0001-01-01T00:00:00') {
                    $scope.tentative_out_time = '';
                }
                else {
                    $scope.tentative_out_time = new Date(resp.data.Ttentative_out_time);
                }
                if (resp.data.Tactual_out_time == '0001-01-01T00:00:00') {
                    $scope.txtactualouttime = '';
                }
                else {
                    $scope.txtactualouttime = new Date(resp.data.Tactual_out_time);
                }
                $scope.cbovisitingofficer_editlist = resp.data.visitingofficerem_list;
                $scope.visitingofficer_name = resp.data.visitingofficer_name;
                $scope.cbovisitingofficer_edit = [];
                if (resp.data.visitingofficer_name != null) {
                    var count = resp.data.visitingofficer_name.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cbovisitingofficer_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.visitingofficer_name[i].employee_gid);
                        $scope.cbovisitingofficer_edit.push($scope.cbovisitingofficer_editlist[indexs]);

                    }
                }
                unlockUI();
            });
        }
  
        $scope.update = function (visitor_gid) {
            if ($scope.cbobranch_name == undefined || $scope.cbobranch_name == null)
            {
                Notify.alert("Select Branch Name", 'warning');
            }
            else if ($scope.cbovisitingofficer_edit == undefined || $scope.cbovisitingofficer_edit == null)
            {
                Notify.alert("Select Visitor Official", 'warning');
            }
            else if ($scope.rbovisittype == undefined || $scope.rbovisittype == null) {
                Notify.alert("Select Visitor Type", 'warning');
            }
            else if ($scope.txtpurposeofvisit == undefined || $scope.txtpurposeofvisit == null) {
                Notify.alert("Enter Purpose Of Visit", 'warning');
            }
            else if ($scope.txtin_time == undefined || $scope.txtin_time == null) {
                Notify.alert("Select In Time", 'warning');
            }
            else if ($scope.tentative_out_time == undefined || $scope.tentative_out_time == null) {
                Notify.alert("Select Tentative Out Time", 'warning');
            }
            else
            {
                var lsbranch_name = "";
                var lsbranch_gid = "";
                if ($scope.cbobranch_name != undefined || $scope.cbobranch_name != null) {
                    lsbranch_name = $('#branch_name :selected').text();
                    lsbranch_gid = $scope.cbobranch_name;
                }
                var params = {
                    visitor_gid: $scope.visitor_gid,
                    branch_name: lsbranch_name,
                    branch_gid: lsbranch_gid,
                    visitingofficer_name: $scope.cbovisitingofficer_edit,
                    visiting_type: $scope.rbovisittype,
                    purpose_of_visit: $scope.txtpurposeofvisit,
                    in_time: $scope.txtin_time,
                    tentative_out_time: $scope.tentative_out_time,
                    visit_date: $scope.txtvisit_date,
                    actual_out_time: $scope.txtactualouttime,
                }
                var url = 'api/Mstvisitor/UpdateVisitor';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstVisitorSummary");
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        
            
        }
        $scope.back = function () {
            $location.url("app/MstVisitorSummary");
        }
        //$scope.add_mobileno = function (visitor_gid) {
        //    if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined)) {
        //        Notify.alert('Enter Mobile Number/Select Status', 'warning');
        //    }
        //    else {
        //         var params = {
        //            visitor_gid : $scope.visitor_gid,
        //            mobile_no: $scope.txtmobile_no,
        //            primary_mobileno: $scope.rdbprimarymobile_no
        //         }

        //         var url = 'api/MstVisitor/PostEditVisitorMobileNo';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'warning',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            mobileno_list();
        //            $scope.txtmobile_no = '';
        //            $scope.rdbprimarymobile_no = '';
        //            $scope.rdbprimarymobile_no == false;
        //        }); 
        //    }
        //}
        //$scope.delete_mobileno = function (visitor2contact_gid) {
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Mobile No ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var params ={
        //                visitor2contact_gid: visitor2contact_gid
        //            }
        //            var url = 'api/MstVisitor/DeletevisitMobileno'; 
        //            lockUI();
        //            SocketService.getparams(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                mobileno_list();
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }
        //    });
        //}
        //function mobileno_list() {
        //    var url = 'api/MstVisitor/GetVisitMobileNo';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.visitormobilenumber_list = resp.data.visitormobileno_list;
        //    });
        //}
//Email Address Multiple Add
        //$scope.add_emailaddress = function () {
        //    if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
        //        Notify.alert('Enter Email Address/Select Status','warning');
        //    }
        //    else {
        //        var params = {
        //            visitor_gid : $scope.visitor_gid,
        //            email_address: $scope.txtemail_address,
        //            primary_emailaddress: $scope.rdbprimaryemail_address,
        //        }
        //        var url = 'api/MstVisitor/PostEditVisitorEmailAddress'; 
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'warning',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
                   
        //            $scope.txtemail_address = '';
        //            $scope.rdbprimaryemail_address = '';
        //            $scope.rdbprimaryemail_address == false;
        //            emailaddress_list();
        //        }); 
        //    }
        //}
        //$scope.delete_emailaddress = function (visitor2email_gid) {
        //    SweetAlert.swal({
        //        title: 'Are you sure?',
        //        text: 'Do You Want To Delete the Email Address ?',
        //        showCancelButton: true,
        //        confirmButtonColor: '#DD6B55',
        //        confirmButtonText: 'Yes, delete it!',
        //        closeOnConfirm: false
        //    }, function (isConfirm) {
        //        if (isConfirm) {
        //            var params = {
        //                visitor2email_gid: visitor2email_gid
        //            }
        //            var url = 'api/MstVisitor/DeleteVisitorEmailAddress';
        //            lockUI();
        //            SocketService.getparams(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                emailaddress_list();
        //            });
        //            SweetAlert.swal('Deleted Successfully!');
        //        }
        //    });
        //}
        //function emailaddress_list() {
        //    var url = 'api/MstVisitor/GetVisitorEmailAddress';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.visitoremailaddress_list = resp.data.visitoremailaddress_list;
        //    });
        //}

//Photo Upload
        $scope.VisitReportPhotoUpload = function () {
            lockUI();
            var fi = document.getElementById('photo');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    frm.append('photo_name', $scope.txtphoto_name);
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "photoformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                var url = 'api/MstVisitor/VisitPhotoUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#photo").val('');
                    unlockUI();
                    if (resp.data.status == true){
                    
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    photo_list();
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.txtphoto_name = '';
        }
        function photo_list() {
            var url = 'api/MstVisitor/GetVisitPhotos ';
            SocketService.get(url).then(function (resp) {
                $scope.UploadphotoList = resp.data.VisitorUploadphotoList;
            });
        }
        $scope.uploadphotocancel = function (visitor2photo_gid) {
        
            var params = {
                visitor2photo_gid: visitor2photo_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Photo ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstVisitor/DeleteVisittmpPhotoList';
                    SocketService.getparams(url, params).then(function (resp) {                        
                        if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        photo_list();
                        unlockUI();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }
        
        $scope.photo_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.UploadphotoList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadphotoList[i].document_path, $scope.UploadphotoList[i].filename);

            }
        }

        function visitorname_list() {
            var params = {
                visitor_gid: $scope.visitor_gid
            }
            
            var url = 'api/MstVisitor/GetVisitorNameList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitorlist = resp.data.visitorname_list;
            });
        }
        // Visitor Details Add
        $scope.add_visitordtl = function () {
            var params = {
                visitor_name: $scope.txtvisitor_name,
                visitoridproof: $scope.cbovisitoridproof,
                visitoridproof_no: $scope.txtvisitoridproof_no,
                temperature: $scope.txttemperature,
                vaccination_status: $scope.cbovacciantion_status,
                visitorcompany_name: $scope.txtvisitorcompany_name,
                spo2: $scope.txt_spo2,
                visitor_email: $scope.txtvisitoremail_address,
                visitor_mobileno: $scope.txtvisitormobile_no
            }
            var url = 'api/MstVisitor/PostVisitorName';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                //mobileno_list();
                photo_list();
                //emailaddress_list();
                visitorname_list();
                $scope.txtvisitor_name = '';
                $scope.cbovisitoridproof = '';
                $scope.txtvisitoridproof_no = '';
                $scope.txttemperature = '';
                $scope.cbovacciantion_status = '';
                $scope.txtvisitorcompany_name = '';
                $scope.txt_spo2 = '';
                $scope.txtvisitoremail_address = '';
                $scope.txtvisitormobile_no = '';
            });

        }

        // Visitor Details Delete
        $scope.delete_visitordtl = function (visitorname_gid) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Visitor Details?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var params = {
                        visitorname_gid: visitorname_gid
                    }
                    var url = 'api/MstVisitor/DeleteVisitorName';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        visitorname_list();
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }
            });
        }

        // Tag Generate
        $scope.TagGenereation = function (visitorname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/generatevisitortag.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                // Calender Popup... //

                vm.calender1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open1 = true;
                };


                lockUI();
                var params = {
                    visitorname_gid: visitorname_gid
                }
                var url = 'api/MstVisitor/GetVisitorTagView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txttag_id = resp.data.tag_id;
                    $scope.txtvalidity_from = resp.data.tag_validity_from;
                    $scope.txtvalidity_from = new Date($scope.txtvalidity_from);
                    $scope.txtvalidity_to = resp.data.tag_validity_to;
                    $scope.txtvalidity_to = new Date($scope.txtvalidity_to);
                    if(resp.data.tag_validity_from=='0001-01-01')
                    {
                        $scope.txtvalidity_from = "";
                    }
                    if (resp.data.tag_validity_to == '0001-01-01') {
                        $scope.txtvalidity_to = "";
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

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

                $scope.onchangetime = function () {
                    var g1 = new Date($scope.txtvalidity_from);
                    var g2 = new Date($scope.txtvalidity_to);
                   
                    if (g1.getTime() == g2.getTime())
                        alert("Both are equal", 'warning');
                    else if (g1.getTime() > g2.getTime())
                        alert("Start Time is greater than End Time", 'warning');
                }

                $scope.generatetags = function () {
                    lockUI();
                    var url = 'api/MstVisitor/UpdateVisitorTag';
                    var params = {
                        visitorname_gid: visitorname_gid,
                        tag_id: $scope.txttag_id,
                        tag_validity_from: $scope.txtvalidity_from,
                        tag_validity_to: $scope.txtvalidity_to,

                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });                                                  

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        visitorname_list();

                    });
                    $modalInstance.close('closed');
                  
                    unlockUI();
                }
            }
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