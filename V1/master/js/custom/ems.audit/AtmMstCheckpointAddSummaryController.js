(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstCheckpointAddSummaryController', AtmMstCheckpointAddSummaryController);

    AtmMstCheckpointAddSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AtmMstCheckpointAddSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstCheckpointAddSummaryController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.checkpointgroup_gid = searchObject.checkpointgroup_gid;
        var checkpointgroup_gid = $scope.checkpointgroup_gid;
        $scope.checkpointgroupadd_gid = searchObject.checkpointgroupadd_gid;
        var checkpointgroupadd_gid = $scope.checkpointgroupadd_gid;
        $scope.IsVisible = false;
        $scope.IsVisible1 = false;
        $scope.IsVisible2 = false;
        $scope.IsVisible3 = false;

        activate();

        function activate() {
            var url = 'api/AtmTrnAuditCreation/TempDeleteAuditee';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/AtmMstCheckpointGroup/TempDeleteCheckpointList';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/AtmMstPositiveConfirmity/GetPositiveConfirmityActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.positiveconfirmity_data = resp.data.positiveconfirmity_list;
                unlockUI();
            });

         
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee1_list = resp.data.employeelist;
                unlockUI();
            });
            //var url = 'api/AtmMstCheckpointGroup/GetCheckListToCheckpoint';
            //SocketService.get(url).then(function (resp) {
            //    $scope.checklist_list = resp.data.checklistcheckpoint_list;
            //});
            var url = 'api/AtmMstRiskcategory/GetRiskcategoryActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.riskcategory_list = resp.data.riskcategory_list;
                unlockUI();
            });


            var params = {
                checkpointgroup_gid: checkpointgroup_gid
            };

            var url = 'api/AtmMstCheckpointGroup/GetCheckpointAdd';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checkpointgroupadd_list = resp.data.checkpointgroupadd_list;
                unlockUI();
            });
           
            var params = {
                checkpointgroup_gid: checkpointgroup_gid
            };

            var url = 'api/AtmMstCheckpointGroup/GetCheckpointGroupName';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cbocheckpointgroup = resp.data.checkpointgroup_name;

                unlockUI();
            });

        }      

        $scope.addcheckpoint = function (checkpointgroup_gid) {
        $scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.IsVisible1 = $scope.IsVisible1 ? false : true;
        $scope.IsVisible2 = false;
        $scope.IsVisible3 = false;
    }

    $scope.changevalueadd = function () {
        var val1 = 0;
        var val2 = 0;
        var val3 = 0;
        var val4 = 0;
        var val5 = 0;
        if ($scope.txtna_score != undefined || $scope.txtna_score != null) {
            val1 = $scope.txtna_score;
        }

        if ($scope.txtno_score != undefined || $scope.txtno_score != null) {
            val2 = $scope.txtno_score;
        }

        if ($scope.txtyes_score != undefined || $scope.txtyes_score != null) {
            val3 = $scope.txtyes_score;
        }

        if ($scope.txtpartial_score != undefined || $scope.txtpartial_score != null) {
            val4 = $scope.txtpartial_score;
        }


        /*val5 = val1 + val2 + val3 + val4;*/
        val5 = val3;

        $scope.txt_totalscore = parseFloat(val5.toFixed(2));

    }
    $scope.submitAdd = function () {

        var lsriskcategory_gid = '';
        var lsriskcategory_name = '';
        if ($scope.cboriskcategory != undefined || $scope.cboriskcategory != null) {
            lsriskcategory_gid = $scope.cboriskcategory.riskcategory_gid;
            lsriskcategory_name = $scope.cboriskcategory.riskcategory_name;
        }

        var lspositiveconfirmity_gid = '';
        var lspositiveconfirmity_name = '';
        if ($scope.cbopositiveconfirmity != undefined || $scope.cbopositiveconfirmity != null) {
            lspositiveconfirmity_gid = $scope.cbopositiveconfirmity.positiveconfirmity_gid;
            lspositiveconfirmity_name = $scope.cbopositiveconfirmity.positiveconfirmity_name;
        }

        //var lscheckpointgroup_gid = '';
        //var lscheckpointgroup_name = '';
        //if ($scope.cbocheckpointgroup != undefined || $scope.cbocheckpointgroup != null) {
        //    lscheckpointgroup_gid = $scope.cbocheckpointgroup.checkpointgroup_gid;
        //    lscheckpointgroup_name = $scope.cbocheckpointgroup.checkpointgroup_name;
        //}
       

        var params = {
            
            positiveconfirmity_gid: lspositiveconfirmity_gid,
            positiveconfirmity_name: lspositiveconfirmity_name,
            riskcategory_gid: lsriskcategory_gid,
            riskcategory_name: lsriskcategory_name,
            checkpointgroup_gid: checkpointgroup_gid,
            checkpoint_intent: $scope.txtcheckpoint_intent,
            checkpoint_description: $scope.txtcheckpoint_description,
            noteto_auditor: $scope.txtnoteto_auditor,
            yes_score: $scope.txtyes_score,
            yes_disposition: $scope.txtyes_disposition,
            no_score: $scope.txtno_score,
            no_disposition: $scope.txtno_disposition,
            partial_score: $scope.txtpartial_score,
            partial_disposition: $scope.txtpartial_disposition,
            na_score: $scope.txtna_score,
            na_disposition: $scope.txtna_disposition,
            total_score: $scope.txt_totalscore,

        }

        var url = 'api/AtmMstCheckpointGroup/PostCheckpointAdd';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
               
                $scope.cboriskcategory = '';
                $scope.cbopositiveconfirmity = '';
                $scope.txtcheckpoint_intent = '';
                $scope.txtcheckpoint_description = '';
                $scope.txtnoteto_auditor = '';
                $scope.txtyes_score = '';
                $scope.txtyes_disposition = '';
                $scope.txtno_score = '';
                $scope.txtno_disposition = '';
                $scope.txtpartial_score = '';
                $scope.txtpartial_disposition = '';
                $scope.txtna_score = '';
                $scope.txtna_disposition = '';
                $scope.txt_totalscore = '';
                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                //$state.go('app.AtmMstCheckpointGroupAdd');

                //$location.url('app/AtmMstCheckpointAddSummary?checkpointgroup_gid=' + checkpointgroup_gid);
                
                //activate();
                location.reload(true);
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
        //    location.reload(true);
        });

        $scope.IsVisible1 = $scope.IsVisible1 ? false : true;
        $scope.IsVisible = $scope.IsVisible ? false : true;

    }



    $scope.edit = function (val) {
        $scope.checkpointgroupadd_gid = val;
        var params = {
            checkpointgroupadd_gid: val
        }

        var url = 'api/AtmMstCheckpointGroup/EditCheckpoint';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
            $scope.cbocheckpointgroup = resp.data.checkpointgroup_name           
            $scope.cboriskcategoryedit = resp.data.riskcategory_gid,
            $scope.cbopositiveconfirmityedit = resp.data.positiveconfirmity_gid,
            $scope.txteditcheckpoint_intent = resp.data.checkpoint_intent,
            $scope.txteditcheckpoint_description = resp.data.checkpoint_description,
            $scope.txteditnoteto_auditor = resp.data.noteto_auditor,
            $scope.txtedityes_score = parseFloat(resp.data.yes_score),
            $scope.txtedityes_disposition = resp.data.yes_disposition,
            $scope.txteditno_score = parseFloat(resp.data.no_score),
            $scope.txteditno_disposition = resp.data.no_disposition,
            $scope.txteditpartial_score = parseFloat(resp.data.partial_score),
            $scope.txteditpartial_disposition = resp.data.partial_disposition,
            $scope.txteditna_score = parseFloat(resp.data.na_score),
            $scope.txteditna_disposition = resp.data.na_disposition,
            $scope.txtedit_totalscore = parseFloat(resp.data.total_score),


            unlockUI();
        });
        $scope.IsVisible2 = true;
        $scope.IsVisible3 = false;
        $scope.IsVisible1 = false;
        //$scope.IsVisible2 = $scope.IsVisible2 ? false : true;
        $scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.id = val;
        activate();

        var url = 'api/AtmMstCheckpointGroup/GetCheckpointCheckList';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.checklist_list = resp.data.checklistcheckpoint_list;
        });
        var url = 'api/AtmMstCheckpointGroup/GetAuditeeSummaryList';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.multipleauditee_list = resp.data.multipleauditee_list;
        });
    }

    $scope.changevalue = function () {
        //var val1 = parseFloat($scope.txteditna_score);
        var val1 = parseFloat($scope.txteditna_score);
        var val2 = parseFloat($scope.txteditno_score);
        var val3 = parseFloat($scope.txtedityes_score);
        var val4 = parseFloat($scope.txteditpartial_score);
        var val5 = parseFloat($scope.txtedit_totalscore);
        if ($scope.txteditna_score != undefined || $scope.txteditna_score != null) {
            val1 = parseFloat($scope.txteditna_score);
        }

        if ($scope.txteditno_score != undefined || $scope.txteditno_score != null) {
            val2 = parseFloat($scope.txteditno_score);
        }

        if ($scope.txtedityes_score != undefined || $scope.txtedityes_score != null) {
            val3 = parseFloat($scope.txtedityes_score);
        }

        if ($scope.txteditpartial_score != undefined || $scope.txteditpartial_score != null) {
            val4 = parseFloat($scope.txteditpartial_score);
        }


        //val5 = val1 + val2 + val3 + val4;
         val5 =  val3;

        $scope.txtedit_totalscore = val5;

    }
    $scope.Update = function (id) {

        var riskcategoryname;
        var riskcategory_index = $scope.riskcategory_list.map(function (e) { return e.riskcategory_gid }).indexOf($scope.cboriskcategoryedit);
        if (riskcategory_index == -1) { riskcategoryname = ''; } else { riskcategoryname = $scope.riskcategory_list[riskcategory_index].riskcategory_name; };
        var positiveconfirmityname;
        var positiveconfirmity_index = $scope.positiveconfirmity_data.map(function (e) { return e.positiveconfirmity_gid }).indexOf($scope.cbopositiveconfirmityedit);
        if (positiveconfirmity_index == -1) { positiveconfirmityname = ''; } else { positiveconfirmityname = $scope.positiveconfirmity_data[positiveconfirmity_index].positiveconfirmity_name; };
        
        var params = {

            checkpointgroupadd_gid: id,           
            riskcategory_gid: $scope.cboriskcategoryedit,
            checkpointgroup_gid: checkpointgroup_gid,
            riskcategory_name: riskcategoryname,
            positiveconfirmity_gid: $scope.cbopositiveconfirmityedit,
            positiveconfirmity_name: positiveconfirmityname,
            checkpoint_intent: $scope.txteditcheckpoint_intent,
            checkpoint_description: $scope.txteditcheckpoint_description,
            noteto_auditor: $scope.txteditnoteto_auditor,
            yes_score: $scope.txtedityes_score,
            yes_disposition: $scope.txtedityes_disposition,
            no_score: $scope.txteditno_score,
            no_disposition: $scope.txteditno_disposition,
            partial_score: $scope.txteditpartial_score,
            partial_disposition: $scope.txteditpartial_disposition,
            na_score: $scope.txteditna_score,
            na_disposition: $scope.txteditna_disposition,
            total_score: $scope.txtedit_totalscore,

        }

        var url = 'api/AtmMstCheckpointGroup/UpdateCheckpoint';
        lockUI()
        SocketService.post(url, params).then(function (resp) {
            if (resp.data.status == true) {
                unlockUI()
                
                //$state.go('app.AtmMstCheckpointGroupAdd');
                //$location.url('app/AtmMstCheckpointAddSummary?checkpointgroup_gid=' + checkpointgroup_gid);
                location.reload(true);
                //activate();
                Notify.alert(resp.data.message, 'success')
            }
            else {
                unlockUI();
                Notify.alert(resp.data.message, 'warning')
            }

        });
        $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
        $scope.IsVisible = $scope.IsVisible ? false : true;      
    }

    $scope.edit_back = function (val1) {
        $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
        //$scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.IsVisible = false;

    }
    $scope.viewback = function () {
        
        $scope.IsVisible3 = $scope.IsVisible3 ? false : true;
        //$scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.IsVisible = false;

    }



    $scope.view = function (val7) {
        //$scope.IsVisible = $scope.IsVisible ? false : true;
        //$scope.IsVisible3 = $scope.IsVisible3 ? false : true;


        var url = 'api/AtmMstCheckpointGroup/EditCheckpoint';
        var params = {
            checkpointgroupadd_gid: val7
        }
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
            $scope.txtcheckpoint1 = resp.data.checkpointgroup_name
            $scope.cboriskcategory1 = resp.data.riskcategory_gid,
              $scope.cboriskcategory1 = resp.data.riskcategory_name,
             $scope.cbopositiveconfirmity1 = resp.data.positiveconfirmity_gid,
                $scope.cbopositiveconfirmity1 = resp.data.positiveconfirmity_name,             
            $scope.txtcheckpoint_intent1 = resp.data.checkpoint_intent,
            $scope.txtcheckpoint_description1 = resp.data.checkpoint_description,
            $scope.txtnoteto_auditor1 = resp.data.noteto_auditor,
            $scope.txtyes_score1 = resp.data.yes_score,
            $scope.txtyes_disposition1 = resp.data.yes_disposition,
            $scope.txtno_score1 = resp.data.no_score,
            $scope.txtno_disposition1 = resp.data.no_disposition,
            $scope.txtpartial_score1 = resp.data.partial_score,
            $scope.txtpartial_disposition1 = resp.data.partial_disposition,
            $scope.txtna_score1 = resp.data.na_score,
            $scope.txtna_disposition1 = resp.data.na_disposition,
            $scope.txt_totalscore1 = resp.data.total_score,


            unlockUI();
        });

        $scope.IsVisible3 = true;
        $scope.IsVisible2 = false;
        $scope.IsVisible1 = false;
        //$scope.IsVisible3 = $scope.IsVisible3 ? false : true;
        $scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.id = val7;
        activate();
        var url = 'api/AtmMstCheckpointGroup/GetCheckListToCheckpointView';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.checklist_list = resp.data.checklistcheckpoint_list;
        });
        //var url = 'api/AtmTrnAuditCreation/GetAuditeeList';
        //SocketService.getparams(url, params).then(function (resp) {
        //    $scope.multipleauditee_list = resp.data.multipleauditee_list;
        //});
        var url = 'api/AtmMstCheckpointGroup/GetAuditeeSummaryList';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.multipleauditee_list = resp.data.multipleauditee_list;
        });
    }

    $scope.backcheckpoint = function (val) {
        $state.go('app.AtmMstCheckpointGroupAdd');
    }

    $scope.checkpointintent = function (checkpointgroupadd_gid, checkpoint_intent, checkpoint_description) {
        var modalInstance = $modal.open({
            templateUrl: '/checkpointintent.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                checkpointgroupadd_gid: checkpointgroupadd_gid
            }
            lockUI();
            var url = 'api/AtmMstCheckpointGroup/GetCheckpointIntent';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcheckpointintent = resp.data.checkpoint_intent;
                $scope.txtcheckpointdescription = resp.data.checkpoint_description;


            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }
    $scope.description = function (checkpointgroupadd_gid, checkpoint_description) {
        var modalInstance = $modal.open({
            templateUrl: '/checkpointdescription.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            'use strict';

            angular
                .module('angle')
            var params = {
                checkpointgroupadd_gid: checkpointgroupadd_gid
            }
            lockUI();
            var url = 'api/AtmMstCheckpointGroup/GetCheckpointDescription';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcheckpointdescription = resp.data.checkpoint_description;

            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }
    $scope.notetoauditor = function (checkpointgroupadd_gid, noteto_auditor) {
        var modalInstance = $modal.open({
            templateUrl: '/notetoauditor.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                checkpointgroupadd_gid: checkpointgroupadd_gid
            }
            lockUI();
            var url = 'api/AtmMstCheckpointGroup/GetCheckpointNotestoAuditor';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtnotetoauditor = resp.data.noteto_auditor;

            });
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
        }
    }



    $scope.deletecheckpoint = function (checkpointgroupadd_gid) {
        var params = {
            checkpointgroupadd_gid: checkpointgroupadd_gid
        }


        SweetAlert.swal({
            title: 'Are you sure?',
            text: 'Do You Want To Delete the Record ?',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                var url = 'api/AtmMstCheckpointGroup/DeleteCheckpointAdd';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        activate();
                    }
                    else {
                        Notify.alert('Error Occurred While Deleting Check Point Group !!!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                });
                SweetAlert.swal('Deleted Successfully!');
            }

        });
    };

    $scope.importexcel = function (checkpointgroup_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/importexcel.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'lg'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {


            var params = {
                checkpointgroup_gid: checkpointgroup_gid,
            }

            $scope.ok = function () {
                $modalInstance.close('closed');
            };

            $scope.downloadtemplate_importexcel = function () {
              
                var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                var filename = "\ImportExcelCheckpoint.xlsx";
                //var phyPath = resp.data.file_path;
                var phyPath = Templateurl + filename;
                var relPath = phyPath.split("EMS");
                var relpath1 = relPath[1].replace("\\", "/");
                var prefix = window.location.protocol + "//";
                var hosts = window.location.host;
                var str = prefix.concat(hosts, relpath1);
                var link = document.createElement("a");
                var name = filename.split('.');
                link.download = name[0];
                link.href = str;
                link.click();
            }

            $scope.excelupload = function (val, val1, name) {

                var fileInput = document.getElementById('fileimport');
                var filePath = fileInput.value;

                $scope.fileinputvalue = filePath;

                // Allowing file type
                var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                if (!allowedExtensions.exec(filePath)) {
                    Notify.alert('File Format Not Supported!', 'warning')
                    $modalInstance.close('closed');
                    //fileInput.value = '';
                }
                else if (filePath.includes("ImportExcelCheckpoint") == false) {
                    Notify.alert('File Name / Template Not Supported!', 'warning')
                    $modalInstance.close('closed');
                }
                else {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('checkpointgroup_gid', checkpointgroup_gid);
                    frm.append('project_flag', "Default");

                    $scope.uploadfrm = frm;
                }
            }

            $scope.uploadexcel = function () {

                if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                    Notify.alert('Kindly Select the Excel file', 'warning')
                }
                else {
                    var url = 'api/AtmMstCheckpointGroup/ImportExcelCheckpoint';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //$location.url('app/AtmMstCheckpointAddSummary?checkpointgroup_gid=' + checkpointgroup_gid)
                            location.reload(true);
                            //activate()
                            $modalInstance.close('closed');
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        $("#fileimport").val('');
                    });
                }

            }

            $scope.uploadexcelcancel = function () {
                $("#fileimport").val('');
            };
        }
    }
    $scope.checklist_add = function () {
        if (($scope.txtchecklist == undefined) || ($scope.txtchecklist == '')) {
            Notify.alert('Enter Check List', 'warning');
        }
        else {

            var params = {
                checkpointgroup_gid: checkpointgroup_gid,      
                checklist_name: $scope.txtchecklist,

            }
            lockUI();
            var url = 'api/AtmMstCheckpointGroup/CreateCheckListToCheckpoint';
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
                checklist();
                $scope.txtchecklist = '';

            });
        }
    }

    function checklist() {
       
        var url = 'api/AtmMstCheckpointGroup/GetCheckListToCheckpoint';
        SocketService.get(url).then(function (resp) {
            $scope.checklistpoint_list = resp.data.checklistcheckpoint_list;
        });
    }
  
    function checkpoint() {
        var params = {
            checkpointgroupadd_gid: checkpointgroupadd_gid,
        }

        var url = 'api/AtmMstCheckpointGroup/GetCheckListToCheckpointView';
        SocketService.getparams(url,params).then(function (resp) {
            $scope.checklist_list = resp.data.checklistcheckpoint_list;
        });
    }
    $scope.checklist_edit = function (checkpointgroupadd_gid) {
        if (($scope.txtchecklist == undefined) || ($scope.txtchecklist == '')) {
            Notify.alert('Enter Check List', 'warning');
        }
        else {

            var params = {
                checkpointgroup_gid: checkpointgroup_gid,
                checkpointgroupadd_gid: checkpointgroupadd_gid,
                checklist_name: $scope.txtchecklist,

            }
            lockUI();
            var url = 'api/AtmMstCheckpointGroup/UpdateCheckListToCheckpoint';
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
                checkpointcheck_editlist(checkpointgroupadd_gid);
                $scope.txtchecklist = '';

            });
        }
    }

    function checkpointcheck_editlist(checkpointgroupadd_gid) {
        var params =
        {
            checkpointgroupadd_gid: checkpointgroupadd_gid,
        }
        var url = 'api/AtmMstCheckpointGroup/GetTempCheckpointCheckList';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.checklist_list = resp.data.checklistcheckpoint_list;
        });
    }
    $scope.checklist_delete = function (checklist2checkpoint,checkpointgroupadd_gid) {
        var params =
            {
                checklist2checkpoint: checklist2checkpoint
            }
        lockUI();
        var url = 'api/AtmMstCheckpointGroup/DeleteChecklist2Checkpoint';
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
            checkpointcheck_editlist(checkpointgroupadd_gid);
            
        });
    }
        $scope.delete_checkpoint = function (checklist2checkpoint) {
        var params =
            {
            checklist2checkpoint: checklist2checkpoint
            }
        var url = 'api/AtmMstCheckpointGroup/DeleteCheckpointList';

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
            checkpoint_list();
        });
    }
    function checkpoint_list() {
        var url = 'api/AtmMstCheckpointGroup/GetCheckpointList';
        SocketService.get(url).then(function (resp) {
            $scope.checklistpoint_list = resp.data.checklistcheckpoint_list;
        });
    }


    $scope.delete_checkpointchecklist = function (checklist2checkpoint) {
        var params =
            {
                checklist2checkpoint: checklist2checkpoint
            }
        var url = 'api/AtmMstCheckpointGroup/DeleteCheckpointList';

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
            checkpointcheck_list();
        });
        }
        $scope.addauditee = function () {
            var lsauditeemaker_gid = '';
            var lsauditeemaker_name = '';
            if ($scope.cboauditeemaker != undefined || $scope.cboauditeemaker != null) {
                lsauditeemaker_gid = $scope.cboauditeemaker.employee_gid;
                lsauditeemaker_name = $scope.cboauditeemaker.employee_name;
            }
            var lsauditeechecker_gid = '';
            var lsauditeechecker_name = '';
            if ($scope.cboauditeechecker != undefined || $scope.cboauditeechecker != null) {
                lsauditeechecker_gid = $scope.cboauditeechecker.employee_gid;
                lsauditeechecker_name = $scope.cboauditeechecker.employee_name;
            }

            if (($scope.cboauditeemaker == '' || $scope.cboauditeemaker == null) || ($scope.cboauditeechecker == '' || $scope.cboauditeechecker == null)) {
                Notify.alert('Kindly Fill Auditee Details', 'warning')
            }
            else {
                var params = {
                    auditeemaker_gid: lsauditeemaker_gid,
                    auditeemaker_name: lsauditeemaker_name,
                    auditeechecker_gid: lsauditeechecker_gid,
                    auditeechecker_name: lsauditeechecker_name,
                }
                var url = 'api/AtmMstCheckpointGroup/PostMultipleAuditee';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        auditee_list();

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cboauditeemaker = '';
                    $scope.cboauditeechecker = '';
                });
            }
        }


        function auditee_list() {

            var url = 'api/AtmMstCheckpointGroup/GetAuditeeList';
            SocketService.get(url).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
            });
        }
        $scope.delete_auditee = function (multipleauditee_gid) {
            var params =
            {
                multipleauditee_gid: multipleauditee_gid
            }
            var url = 'api/AtmMstCheckpointGroup/DeleteAuditeeList';

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
                deleteauditee_list();
            });
        }
        function deleteauditee_list() {
            var url = 'api/AtmMstCheckpointGroup/GetAuditeeList';
            SocketService.get(url).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
            });
        }
        $scope.approvalinformation = function (checkpointgroupadd_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Approvalinformation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    checkpointgroupadd_gid: checkpointgroupadd_gid
                }
                var url = 'api/AtmMstCheckpointGroup/GetAuditeeCheckpointSummaryList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.multipleauditee_list = resp.data.multipleauditee_list;
                    unlockUI();

                });
              
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.add_auditee = function (checkpointgroupadd_gid) {
            var lsauditeemaker_gid = '';
            var lsauditeemaker_name = '';
            if ($scope.cboauditeemaker != undefined || $scope.cboauditeemaker != null) {
                lsauditeemaker_gid = $scope.cboauditeemaker.employee_gid;
                lsauditeemaker_name = $scope.cboauditeemaker.employee_name;
            }
            var lsauditeechecker_gid = '';
            var lsauditeechecker_name = '';
            if ($scope.cboauditeechecker != undefined || $scope.cboauditeechecker != null) {
                lsauditeechecker_gid = $scope.cboauditeechecker.employee_gid;
                lsauditeechecker_name = $scope.cboauditeechecker.employee_name;
            }

            if (($scope.cboauditeemaker == '' || $scope.cboauditeemaker == null) || ($scope.cboauditeechecker == '' || $scope.cboauditeechecker == null)) {
                Notify.alert('Kindly Fill Auditee Details', 'warning')
            }
            else {
                var params = {
                    auditeemaker_gid: lsauditeemaker_gid,
                    auditeemaker_name: lsauditeemaker_name,
                    auditeechecker_gid: lsauditeechecker_gid,
                    auditeechecker_name: lsauditeechecker_name,
                    checkpointgroupadd_gid: checkpointgroupadd_gid

                }
                var url = 'api/AtmMstCheckpointGroup/MultipleAuditeeEdit';
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
                    auditeeedit_list(checkpointgroupadd_gid);
                    $scope.cboauditeemaker = '';
                    $scope.cboauditeechecker = '';
                });
            }

        }
        $scope.editdelete_auditee = function (multipleauditee_gid) {
            var params =
            {
                multipleauditee_gid: multipleauditee_gid
            }
            var url = 'api/AtmMstCheckpointGroup/DeleteAuditee';

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
                auditeeedit_list();
            });
        }
        function auditeeedit_list(checkpointgroupadd_gid) {
            var params =
            {
                checkpointgroupadd_gid: checkpointgroupadd_gid,
            }
            var url = 'api/AtmMstCheckpointGroup/GetTempAssignedAuditeeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.multipleauditee_list = resp.data.multipleauditee_list;
            });
        }
}

})();
