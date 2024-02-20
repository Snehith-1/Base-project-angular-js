(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingIndividualSBAverificationController', MstSAOnboardingIndividualSBAverificationController);

    MstSAOnboardingIndividualSBAverificationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstSAOnboardingIndividualSBAverificationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
    /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingIndividualSBAverificationController';

       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.sacontact_gid = searchObject.lssacontact_gid;
        var sacontact_gid = $scope.sacontact_gid;

        activate();
        function activate() { 
            lockUI(); 
            var url = 'api/MstSAOnboardingIndividual/TempDeleteMobileNo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/TempEmailAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempDocuments';
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/TempProspects';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingIndividual/TempPanDoc';
            SocketService.get(url).then(function (resp) {
            });

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
              $event.preventDefault();
              $event.stopPropagation();
              vm.open1 = true;
            };
            vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
            };
           

            lockUI(); 
            var url = 'api/MstSAOnboardingInstitution/GetRMName';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                
                $scope.txtRM = resp.data.reporting_manager;
            });   
            var params = {

                satype_gid: $scope.cbosatype,
                saentitytype_gid: $scope.cbosaentitytype,

                designation_gid: $scope.cboDesignation,
                designation_type: $scope.cboDesignation,
                

                sadocumentlist_gid: $scope.cbosadocument,
                sadocumentlist_name: $scope.cbosadocument
            }
          
            var url = 'api/MstSAOnboardingInstitution/GetDropDown';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_salist = resp.data.satype_list;
            });

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadd_list = resp.data.saentitytype_list;
            });

            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadddoc_list = resp.data.sadocument_list;
            });

            var url = 'api/MstApplicationAdd/PANAbsenceReasonList';
            SocketService.get(url).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
            });
            var param = {
                sacontact_gid: sacontact_gid
            }

            var url = 'api/MstSAOnboardingIndividual/GetSaChequeDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sachequedocument_list = resp.data.sachequedocument_list;
            });

            var url = 'api/MstSAOnboardingIndividual/GetMobileNoEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualmobileno_list = resp.data.Sacontactmobileno_list;
            });

            var url = 'api/MstSAOnboardingIndividual/GetEmailAddressEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardemailaddress_list = resp.data.saOnboardemailaddress_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetProspectsEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualprospects_list = resp.data.saOnboardProspects_list; 
            });
            var url = 'api/MstSAOnboardingIndividual/GetAddressEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.saOnboardaddress_list;
            });
            var url = 'api/MstSAOnboardingIndividual/GetDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lrfilename = resp.data.filename;
                $scope.lrfilepath = resp.data.filepath;
                $scope.individualupload_list = resp.data.saOnboardDocument_list;
            });

            //businessverification
            var url = 'api/MstSAOnboardingBussDevtVerification/GetEmployeelist';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.rmemployeelists;
            });
            //var url = 'api/MstSAOnboardingBussDevtVerification/TagRmLoad';
            //SocketService.get(url).then(function (resp) {
            //    $scope.RM_List = resp.data.Rm_Grp;
            //});
           
            var url = 'api/MstSAOnboardingBussDevtVerification/IndividualDetailsEdit';
    
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                //$scope.cbopanstatus = resp.data.pan_status;
                //if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                //    $scope.havepan = true;
                //    $scope.havenotpan = false;
                //}
                //else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                //    $scope.havenotpan = true;
                //    $scope.havepan = false;
                //}
                $scope.cbopanstatus = resp.data.pan_status;
                if (resp.data.sa_pannumber != null || resp.data.sa_pannumber != "") {
                    $scope.cbopanstatus = 'Customer Submitting PAN';
                }
                else {
                    $scope.cbopanstatus = 'Customer Submitting Form 60';
                }
                if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                    $scope.havepan = true;
                    $scope.havenotpan = false;
                }
                else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                    $scope.havenotpan = true;
                    $scope.havepan = false;
                }
                $scope.lblrecdsce = resp.data.recordsource;
                $scope.satype_name = resp.data.satype_name;
                $scope.saentitytype_name = resp.data.saentitytype_name;
                $scope.txtsafirst_name = resp.data.sa_firstname;
                $scope.txtsamiddle_name = resp.data.sa_middlename;
                $scope.txtsalast_name = resp.data.sa_lastname;
                $scope.txtpan_no = resp.data.sa_pannumber;
                $scope.txtcredited_date = resp.data.sa_appcrediteddate;
                $scope.txtindividual_aadharnumber = resp.data.sa_aadharnumber;           
                $scope.txtutr_number = resp.data.sa_apputr;
                $scope.txtcredited_amount = resp.data.sa_appcreditedamount;                
                $scope.lblifsc_code = resp.data.saifsc_code;
                $scope.lblbankaccount_number = resp.data.saaccount_number;
                $scope.lblconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                $scope.lblaccountholder_name = resp.data.saaccountholder_name;
                $scope.lblcancelledcheque_number = resp.data.sacanccheque_number;
                $scope.lblbranch_name = resp.data.sabranch_name;
                $scope.lblbank_name = resp.data.sabank_name;
                $scope.lblmicr = resp.data.micr;
                $scope.lblcity = resp.data.city;
                $scope.lblstate = resp.data.state;
                $scope.lbldistrict = resp.data.district;
                $scope.lblbranchaddress = resp.data.branch_address;
                $scope.txtcrime_check = resp.data.crime_check;
                $scope.txtbureau_check = resp.data.bureau_check;
                $scope.txtremarks = resp.data.remarks;
                $scope.cbotrainingstatus = resp.data.training_status;
                $scope.cborm = resp.data.rm_tagging;
                $scope.txtgender = resp.data.gender;
                $scope.txtreferredby = resp.data.referred_by;
                $scope.txtutrno = resp.data.utr_no;
                $scope.present_occupation = resp.data.present_occupation;
                $scope.work_experience = resp.data.work_experience;
                $scope.Expagri_business = resp.data.Expagri_business;

                var trueflag = false;
                if (resp.data.update_flag = 'Y')
                {
                   
                    
                }
                else
                {
                    trueflag = false;
                }
                              
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetPANForm60EditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
            });
            
            var url = 'api/MstSAOnboardingIndividual/EditPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereasonsa_list;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].check_status == true) {
                        $scope.panabsencereason_list[i].checked = true;
                    }
                }
            });

            var url = 'api/MstSAOnboardingIndividual/ContactPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanabsencereason_list = resp.data.contactpanabsencereasonsa_list;
            });
         
           unlockUI();
           $scope.havenotpan = false;
           $scope.havepan = false;
           $scope.view_nopanreasons = false;


           

        }
        $scope.reject_individual_RM = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectrequest.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.rejectSubmit = function () {
                    var params = {
                        sacontact_gid: sacontact_gid,
                        rejected_remarks: $scope.txtreject_remarks,
                        training_status: $scope.cbotrainingstatus,
                        remarks: $scope.txtremarks
                    }
                    var url = 'api/MstSAOnboardingBussDevtVerification/IndividualRegisterationRejected';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            $state.go("app.MstSAOnboardingBussDevtVerificationSummary");
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                        }
                    });
                    $state.go("app.MstSAOnboardingBussDevtVerificationSummary");
                }
                //}
            }        }
         // PAN Change

         $scope.change_pan = function (cbopanstatus) {
            if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                $scope.havepan = true;
                $scope.havenotpan = false;
                angular.forEach($scope.panabsencereason_list, function (val) {
                    val.checked = false;
                });
                var url = 'api/MstSAOnboardingIndividual/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
                    $scope.contactpanform60_list = '';
                });               
            }
            else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                $scope.havenotpan = true;
                $scope.havepan = false;
                $scope.view_nopanreasons = true;
                $scope.txtpan_no = '';
                $scope.panvalidation = false;
                $scope.txtfirst_name = '';
                $scope.txtmiddle_name = '';
                $scope.txtlast_name = '';
            }
            else {
                $scope.havepan = false;
                $scope.havenotpan = false;
            }
        }

        $scope.pandtl_submit = function () {

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {

                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });

            var params = {
                panabsencereason_selectedlist: panabsencereason_selectedList,
            }
            var url = 'api/MstSAOnboardingIndividual/PostPANAbsenceReasons';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.view_nopanreasons = false;
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

            });

        }
        $scope.pandtl_close = function () {
            $scope.view_nopanreasons = false;
        }

        $scope.IndividualPANForm60DocumentUpload = function (val, val1, name) {
            lockUI();
            var frm = new FormData();

            for (i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }



            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstSAOnboardingIndividual/PANForm60DocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#panform60file").val('');
                        $scope.txtindividualpanform60_document = ''
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sacontact_gid: sacontact_gid
                    }
                    var url = 'api/MstSAOnboardingIndividual/GetPANForm60TempList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
                    });

                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }

        }

        $scope.IndividualPANForm60DocumentDelete = function (sacontact2panform60_gid) {

            var params = {
                sacontact2panform60_gid: sacontact2panform60_gid
            }
            lockUI();
            var url = 'api/MstSAOnboardingIndividual/PANForm60Delete';
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
                var param = {
                    sacontact_gid: $scope.sacontact_gid
                }
                var url = 'api/MstSAOnboardingIndividual/GetPANForm60TempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60sa_list;
                });
                unlockUI();
            });
        }


        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                       // $scope.txtbank_address = resp.data.result.address;
                       // $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                       // $scope.txtbank_address = '';
                        //$scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }


        $scope.BankAccValidation = function () {
            if ($scope.txtbankaccount_number == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtaccountholder_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtaccountholder_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        // $scope.onchangeifsc_code = function () {
        //         $scope.txtbank_name='bank name';
        //         $scope.txtbranch_name='branch name';
        //     var params = {
        //         ifsc_code: $scope.txtifsc_code
        //     }
        //   //  var url = 'api/';
        //     SocketService.getparams(url, params).then(function (resp) {
        //         $scope.txtbank_name='bank name';
        //         $scope.txtbranch_name='branch name';
        //     });
        // }

        //Bussverification
        $scope.verify_individual = function () {
            var panabsencereasons_checked = false;

            for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                if ($scope.panabsencereason_list[i].checked == true) {
                    panabsencereasons_checked = true;
                    break;
                }
            }

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {
                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });
            var satypename = $('#satype_name :selected').text();
            var saentitytypename = $('#saentitytype_name :selected').text();
            

            if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
                Notify.alert('Kindly Enter PAN Value', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
                Notify.alert('Kindly Upload Form 60 Document', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
                Notify.alert('Kindly Select Reasons for Uploading Form 60 Document', 'warning')
            }
            else {

                var params = {
                    pan_status: $scope.cbopanstatus,
                    satype_gid: $scope.cbosatype,
                    satype_name: satypename,
                    saentitytype_gid: $scope.cbosaentitytype,
                    saentitytype_name: saentitytypename,
                    sa_reportingmanager: $scope.txtRM,
                    sacontact_gid: sacontact_gid,
                    sa_firstname: $scope.txtsafirst_name,
                    sa_middlename: $scope.txtsamiddle_name,
                    sa_lastname: $scope.txtsalast_name,
                    sa_pannumber: $scope.txtpan_no,
                    sa_aadharnumber: $scope.txtindividual_aadharnumber,
                    sa_apputr: $scope.txtutr_number,
                    saappcrediteddate: $scope.txtcredited_date,
                    sa_appcreditedamount: $scope.txtcredited_amount,
                    saifsc_code: $scope.txtifsc_code,
                    saaccount_number: $scope.txtbankaccount_number,
                    confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                    saaccountholder_name: $scope.txtaccountholder_name,
                    sacanccheque_number: $scope.txtcancelledcheque_number,
                    sabank_name: $scope.txtbank_name,
                    sabranch_name: $scope.txtbranch_name,
                    city: $scope.txtcity,
                    district: $scope.txtdistrict,
                    state: $scope.txtstate,
                    micr: $scope.txtmicr,
                    branch_address: $scope.txtbranch_address,
                    panabsencereason_selectedlist: panabsencereason_selectedList,
                    rm_tagging_id: $scope.cborm.employee_gid,
                    rm_tagging_name: $scope.cborm.employee_name,
                    bureau_check: $scope.txtbureau_check,
                    crime_check: $scope.txtcrime_check,
                    training_status: $scope.cbotrainingstatus,
                    remarks: $scope.txtremarks,
                    verify_flag:'Y'
                }
                console.log(params);
                var url = 'api/MstSAOnboardingBussDevtVerification/IndividualVerify';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstSAOnboardingIndividualSummary');
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
        $scope.update_individual_RM = function () {
            if ($scope.txtremarks == undefined || $scope.txtremarks == null || $scope.txtremarks == "") {
                Notify.alert('Please enter remarks', 'warning')
            }
            else {
                var lsassessmentagency_gid = '';
                var lsassessmentagency_name = '';

                if ($scope.cboemployee != undefined || $scope.cboemployee != null) {
                    lsassessmentagency_gid = $scope.cboemployee
                    lsassessmentagency_name = $('#RM :selected').text()
                }

                var params = {
                    sacontact_gid: sacontact_gid,
                    rm_tagging_id: $scope.cboemployee,
                    rm_tagging_name: lsassessmentagency_name,
                    tagged_remarks: $scope.txtremarks


                }

                console.log(params);
                var url = 'api/MstSAOnboardingBussDevtVerification/IndividuaRMUpdate';
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
                    $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
                });

            }
        }


        //Bussverification
        $scope.update_individual_BussVerification = function () {
            var panabsencereasons_checked = false;

            for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                if ($scope.panabsencereason_list[i].checked == true) {
                    panabsencereasons_checked = true;
                    break;
                }
            }

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {
                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });
            var satypename = $('#satype_name :selected').text();
            var saentitytypename = $('#saentitytype_name :selected').text();
          
            //var rmname;
            //var rmname_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cborm);
            //if (rmname_index == -1) { rmname = ''; } else { rmname = $scope.employee_list[rmname_index].employee_name; };

            if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
                Notify.alert('Kindly Enter PAN Value', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
                Notify.alert('Kindly Upload Form 60 Document', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
                Notify.alert('Kindly Select Reasons for Uploading Form 60 Document', 'warning')
            }
            else {

                var params = {
                    pan_status: $scope.cbopanstatus,
                    satype_gid: $scope.cbosatype,
                    satype_name: satypename,
                    saentitytype_gid: $scope.cbosaentitytype,
                    saentitytype_name: saentitytypename,
                    sa_reportingmanager: $scope.txtRM,
                    sacontact_gid: sacontact_gid,
                    sa_firstname: $scope.txtsafirst_name,
                    sa_middlename: $scope.txtsamiddle_name,
                    sa_lastname: $scope.txtsalast_name,
                    sa_pannumber: $scope.txtpan_no,
                    sa_aadharnumber: $scope.txtindividual_aadharnumber,
                    sa_apputr: $scope.txtutr_number,
                    saappcrediteddate: $scope.txtcredited_date,
                    sa_appcreditedamount: $scope.txtcredited_amount,
                    saifsc_code: $scope.txtifsc_code,
                    saaccount_number: $scope.txtbankaccount_number,
                    confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                    saaccountholder_name: $scope.txtaccountholder_name,
                    sacanccheque_number: $scope.txtcancelledcheque_number,
                    sabank_name: $scope.txtbank_name,
                    sabranch_name: $scope.txtbranch_name,
                    city: $scope.txtcity,
                    district: $scope.txtdistrict,
                    state: $scope.txtstate,
                    micr: $scope.txtmicr,
                    branch_address: $scope.txtbranch_address,
                    panabsencereason_selectedlist: panabsencereason_selectedList,
                    rm_tagging_id: $scope.cborm.employee_gid,
                    rm_tagging_name: $scope.cborm.employee_name,
                    bureau_check: $scope.txtbureau_check,
                    crime_check: $scope.txtcrime_check,
                    training_status: $scope.cbotrainingstatus,
                    remarks: $scope.txtremarks
                }
                console.log(params);
                var url = 'api/MstSAOnboardingBussDevtVerification/IndividualUpdate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
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



        $scope.update_individual =function () {
            var panabsencereasons_checked = false;

            for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                if ($scope.panabsencereason_list[i].checked == true) {
                    panabsencereasons_checked = true;
                    break;
                }
            }

            var panabsencereason_selectedList = [];
                angular.forEach($scope.panabsencereason_list, function (val) {
                    if (val.checked == true) {
                        var panabsencereason = val.panabsencereason;
                        panabsencereason_selectedList.push(panabsencereason);
                    }

                });
                var satypename = $('#satype_name :selected').text();
                var saentitytypename = $('#saentitytype_name :selected').text();


                if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
                    Notify.alert('Kindly Enter PAN Value', 'warning')
                }
                else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
                    Notify.alert('Kindly Upload Form 60 Document', 'warning')
                }
                else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
                    Notify.alert('Kindly Select Reasons for Uploading Form 60 Document', 'warning')
                }
                else {

                    var params = {
                        pan_status: $scope.cbopanstatus,
                        satype_gid: $scope.cbosatype,
                        satype_name: satypename,
                        saentitytype_gid: $scope.cbosaentitytype,
                        saentitytype_name: saentitytypename,
                        sa_reportingmanager: $scope.txtRM,
                        sacontact_gid: sacontact_gid,
                        sa_firstname: $scope.txtsafirst_name,
                        sa_middlename: $scope.txtsamiddle_name,
                        sa_lastname: $scope.txtsalast_name,
                        sa_pannumber: $scope.txtpan_no,
                        sa_aadharnumber: $scope.txtindividual_aadharnumber,
                        sa_apputr: $scope.txtutr_number,
                        saappcrediteddate: $scope.txtcredited_date,
                        sa_appcreditedamount: $scope.txtcredited_amount,
                        saifsc_code: $scope.txtifsc_code,
                        saaccount_number: $scope.txtbankaccount_number,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                        saaccountholder_name: $scope.txtaccountholder_name,
                        sacanccheque_number: $scope.txtcancelledcheque_number,
                        sabank_name: $scope.txtbank_name,
                        sabranch_name: $scope.txtbranch_name,
                        city: $scope.txtcity,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        micr: $scope.txtmicr,
                        branch_address: $scope.txtbranch_address,
                        panabsencereason_selectedlist: panabsencereason_selectedList
                    }
                    console.log(params);
                    var url = 'api/MstSAOnboardingIndividual/IndividualUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go('app.MstSAOnboardingIndividualSummary');
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
        $scope.save_individual =function () {
            var params = {
                satype_name : $scope.cbosatype.satype_name,
                satype_gid : $scope.cbosatype.satype_gid,
                saentitytype_name :$scope.cbosaentitytype.saentitytype_name,
                saentitytype_gid :$scope.cbosaentitytype.saentitytype_gid,
                sacontact_person_first_name :$scope.txtsacontact_person_first_name,
                sacontact_person_middle_name:$scope.txtsacontact_person_middle_name,
                sacontact_person_last_name: $scope.txtsacontact_person_last_name,
                individual_pannumber: $scope.txtindividual_pannumber,
                individual_aadharnumber :$scope.txtindividual_aadharnumber,
                sa_apputr: $scope.txtutr_number,
                credited_date: $scope.txtcredited_date,
                credited_amount:$scope.txtcredited_amount,
                ifsc_code:$scope.txtifsc_code,
                bankaccount_number:$scope.txtbankaccount_number,
                accountholder_name: $scope.txtaccountholder_name,
                cancelledchequenumber_name :$scope.txtcancelledchequenumber_name,
                bank_name:$scope.txtbank_name,
                branch_name:$scope.txtbranch_name
            }
            console.log(params);
           // var url = 'api/';
           // lockUI();
            SocketService.post(url, params).then(function (resp) {
             //   unlockUI();
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
            });
        $state.go('app.MstSAOnboardingIndividualSummary');
        }
        $scope.back = function () {
            $state.go('app.MstSAOnboardingBussDevtVerificationIndividualSummary');
        }
//Form 60
        $scope.form60Upload = function () {
          //  lockUI();
            var fi = document.getElementById('file');
                var frm = new FormData();
                frm.append(fi.files.name, fi.files)
                $scope.uploadfrm = frm;
               // var url = 'api/';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
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
                  //  unlockUI();
                }); 
        }
//Mobile number Multiple Add
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile Number/Select Status', 'warning');
            }
            else {
                 var params = {
                    samobile_no: $scope.txtmobile_no,
                    saprimary_status: $scope.rdbprimarymobile_no,
                    sacontact_gid: $scope.sacontact_gid,
                    sawhatsapp_no: $scope.rdbwhatsappmobile_no
                }
                var url = 'api/MstSAOnboardingIndividual/MobileNumberAddInEdit';
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
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                }); 
            }
        }
        $scope.delete_mobileno = function (sacontact2mobileno_gid) {
            var params =
                {
                    sacontact2mobileno_gid: sacontact2mobileno_gid
                }
                var url = 'api/MstSAOnboardingIndividual/DeleteMobileNo';
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
                mobileno_list();
            }); 
        }
        function mobileno_list() {
            var param = {
                sacontact_gid: $scope.sacontact_gid
            }

            var url = 'api/MstSAOnboardingIndividual/GetMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualmobileno_list = resp.data.Sacontactmobileno_list;
            }); 
        }
//Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {
                var params = {
                    saemail_address: $scope.txtemail_address,
                    saprimary_status: $scope.rdbprimaryemail_address,
                    sacontact_gid: $scope.sacontact_gid
                }
                var url = 'api/MstSAOnboardingIndividual/PostEmailAddressInEdit';
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
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                }); 
            }
        }
        $scope.delete_emailaddress = function (sacontact2email_gid) {
            var params =
                {
                    sacontact2email_gid: sacontact2email_gid
                }
                var url = 'api/MstSAOnboardingIndividual/DeleteEmailAddress';
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
                emailaddress_list();
            });
        }
        function emailaddress_list() {
            var param= {
                sacontact_gid: $scope.sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetEmailAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.saOnboardemailaddress_list = resp.data.saOnboardemailaddress_list;
            }); 
        }
//Prospects Multiple Add
        $scope.add_prospects = function () {
            if (($scope.txtlead_name == undefined) || ($scope.txtlead_name == '') || ($scope.txtsector_name == undefined) || ($scope.txtsector_name == '')) {
                Notify.alert('Enter Lead Name/Sector','warning');
            }
            else 
            {
            var params={
                salead_name : $scope.txtlead_name,
                sacontact_gid: $scope.sacontact_gid,
                sasector_industry : $scope.txtsector_name
            }
           
            var url = 'api/MstSAOnboardingIndividual/AddProspectsInEdit';
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
            prospects_list();
            }); 
        }
           
            $scope.txtlead_name = '',
            $scope.txtsector_name = ''
        }
        function prospects_list() {
            var param= {
                sacontact_gid: $scope.sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetProspectsTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualprospects_list = resp.data.saOnboardProspects_list; 
            });
        }
        $scope.delete_prospects = function (saprospects_gid) {
            var params =
                {
                    saprospects_gid: saprospects_gid
                }
                var url = 'api/MstSAOnboardingIndividual/DeleteProspects';
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
                prospects_list();
            }); 
        }
//Address Multiple Add
        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                $scope.addresstype_list = resp.data.addresstype_list;
                });
                var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/MstSAOnboardingIndividual/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state;
                    });
                   
                }
                $scope.getGeoCoding = function () {
                    if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
                }

                $scope.txtcountry = "India";

                $scope.addressSub = function () {
                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        city: $scope.txtcity,
                        taluka: $scope.txttaluka,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        sacontact_gid: $scope.sacontact_gid,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/MstSAOnboardingIndividual/PostAddressInEdit';
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
                        address_list();
                    });

                    $modalInstance.close('closed');
                }
            }
        }
        $scope.delete_address = function (sacontact2address_gid) {
            var params =
                {
                    sacontact2address_gid: sacontact2address_gid
                }
                var url = 'api/MstSAOnboardingIndividual/DeleteAddress';
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
                address_list();
            }); 
        }
        function address_list() {
            var param= {
                sacontact_gid: $scope.sacontact_gid
            }
            var url = 'api/MstSAOnboardingIndividual/GetAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.saOnboardaddress_list;
            }); 
        }   
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
//Document Multiple Add
$scope.saindivdualdoc_downloads = function (val1, val2) {
    DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadallbureau = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.documentbureauviewer = function (val1, val2) {
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

$scope.downloads = function (val1, val2) {
    DownloaddocumentService.Downloaddocument(val1, val2);
}

        $scope.individualDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
                $("#companyfile").val('');
                Notify.alert('Kindly Enter the Document Title/ID', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbocompanydocumentname.sadocumentlist_name);
                frm.append('sadocumentlist_gid', $scope.cbocompanydocumentname.sadocumentlist_gid);
                frm.append('document_id', $scope.txtdocument_id);
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstSAOnboardingIndividual/AddDocuments';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.individualupload_list = resp.data.saOnboardDocument_list;
                        unlockUI();
                        $("#companyfile").val('');
                        $scope.cbocompanydocumentname = "";
                        $scope.txtdocument_id = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var param= {
                                sacontact_gid: $scope.sacontact_gid  
                            }
                            var url = 'api/MstSAOnboardingIndividual/GetDocumentTempList';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.individualupload_list = resp.data.saOnboardDocument_list;
                            });

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
        }
        $scope.delete_individualdocument = function (sadocument_gid) {
            lockUI();
            var params = {
                sadocument_gid: sadocument_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteDocuments';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var param= {
                    sacontact_gid: $scope.sacontact_gid  
                }
                var url = 'api/MstSAOnboardingIndividual/GetDocumentTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualupload_list = resp.data.saOnboardDocument_list;
            });
                unlockUI();
            });
        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    longitude: longitude,
                    latitude: latitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


      
        //Cancel Cheque
        $scope.UploadDocument = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstSAOnboardingIndividual/SaInstCancelChequeUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        var url = 'api/Kyc/ChequeOCR';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            if (resp.data.statusCode == 101) {
                                $scope.txtaccountholder_name = resp.data.result.name[0];
                                $scope.txtbankaccount_number = resp.data.result.accNo;
                                $scope.txtconfirmbankacct_no = resp.data.result.accNo;
                                $scope.txtbank_name = resp.data.result.bank;
                                $scope.txtcancelledcheque_number = resp.data.result.chequeNo;
                                $scope.txtifsc_code = resp.data.result.ifsc;
                                $scope.txtmicr = resp.data.result.micr;
                                $scope.txtbranch_address = resp.data.result.bankDetails.address;
                                $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                                $scope.txtcity = resp.data.result.bankDetails.city;
                                $scope.txtdistrict = resp.data.result.bankDetails.district;
                                $scope.txtstate = resp.data.result.bankDetails.state;
                            }
                            else {
                                Notify.alert('Error in fetching values from document..!', 'warning');
                            }
                        });

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
                    $("#file").val('');
                    $scope.uploadfrm = undefined;
                    chequedocument_list();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }

        }

        function chequedocument_list() {
            var url = 'api/MstSAOnboardingIndividual/GetSaChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.sachequedocument_list = resp.data.sachequedocument_list;
            });
        }

        $scope.delete_document = function (individualcancelchequeupload_gid) {
            lockUI();
            var params = {
                individualcancelchequeupload_gid: individualcancelchequeupload_gid
            }
            var url = 'api/MstSAOnboardingIndividual/ChequeDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentupload_list = resp.data.documentupload_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                chequedocument_list();
                unlockUI();
            });
        }

           // PAN Change

        //    $scope.change_pan = function (cbopanstatus) {
        //     if ($scope.cbopanstatus == 'Customer Submitting PAN') {
        //         $scope.havepan = true;
        //         $scope.havenotpan = false;
        //         angular.forEach($scope.panabsencereason_list, function (val) {
        //             val.checked = false;
        //         });
        //         var url = 'api/MstApplicationAdd/GetPANForm60List';
        //         SocketService.get(url).then(function (resp) {
        //             $scope.contactpanform60_list = resp.data.contactpanform60_list;
        //             $scope.contactpanform60_list = '';
        //         });
        //     }
        //     else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
        //         $scope.havenotpan = true;
        //         $scope.havepan = false;
        //         $scope.view_nopanreasons = true;
        //         $scope.txtpan_no = '';
        //         $scope.panvalidation = false;
        //         $scope.txtfirst_name = '';
        //         $scope.txtmiddle_name = '';
        //         $scope.txtlast_name = '';
        //     }
        //     else {
        //         $scope.havepan = false;
        //         $scope.havenotpan = false;
        //     }
        // }

        // $scope.pandtl_submit = function () {

        //     var panabsencereason_selectedList = [];
        //     angular.forEach($scope.panabsencereason_list, function (val) {

        //         if (val.checked == true) {
        //             var panabsencereason = val.panabsencereason;
        //             panabsencereason_selectedList.push(panabsencereason);
        //         }

        //     });

        //     var params = {
        //         panabsencereason_selectedlist: panabsencereason_selectedList,
        //     }
        //     var url = 'api/MstApplicationAdd/PostPANAbsenceReasons';
        //     lockUI();
        //     SocketService.post(url, params).then(function (resp) {
        //         unlockUI();
        //         if (resp.data.status == true) {
        //             $scope.view_nopanreasons = false;
        //             Notify.alert(resp.data.message, {
        //                 status: 'success',
        //                 pos: 'top-center',
        //                 timeout: 3000
        //             });

        //         }
        //         else {
        //             Notify.alert(resp.data.message, {
        //                 status: 'warning',
        //                 pos: 'top-center',
        //                 timeout: 3000
        //             });

        //         }

        //     });

        // }
        // $scope.pandtl_close = function () {
        //     $scope.view_nopanreasons = false;
        // }

        // $scope.IndividualPANForm60DocumentUpload = function (val, val1, name) {
        //     lockUI();
        //     var frm = new FormData();

        //     for (i = 0; i < val.length; i++) {
        //         var item = {
        //             name: val[i].name,
        //             file: val[i]
        //         };
        //         frm.append('fileupload', item.file);
        //         frm.append('file_name', item.name);
        //     }



        //     $scope.uploadfrm = frm;
        //     if ($scope.uploadfrm != undefined) {
        //         var url = 'api/MstApplicationAdd/PANForm60DocumentUpload';
        //         SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
        //             $("#file").val('');
        //             unlockUI();
        //             if (resp.data.status == true) {
        //                 Notify.alert(resp.data.message, {
        //                     status: 'success',
        //                     pos: 'top-center',
        //                     timeout: 3000
        //                 });
        //                 $("#panform60file").val('');
        //                 $scope.txtindividualpanform60_document = ''
        //             }
        //             else {
        //                 Notify.alert(resp.data.message, {
        //                     status: 'warning',
        //                     pos: 'top-center',
        //                     timeout: 3000
        //                 });
        //             }

        //             var url = 'api/MstApplicationAdd/GetPANForm60List';
        //             SocketService.get(url).then(function (resp) {
        //                 $scope.contactpanform60_list = resp.data.contactpanform60_list;
        //             });

        //             unlockUI();
        //         });
        //     }
        //     else {
        //         alert('Please select a file.')
        //     }

        // }

        // $scope.IndividualPANForm60DocumentDelete = function (contact2panform60_gid) {

        //     var params = {
        //         contact2panform60_gid: contact2panform60_gid
        //     }
        //     lockUI();
        //     var url = 'api/MstApplicationAdd/PANForm60Delete';
        //     SocketService.getparams(url, params).then(function (resp) {

        //         if (resp.data.status == true) {

        //             Notify.alert(resp.data.message, {
        //                 status: 'success',
        //                 pos: 'top-center',
        //                 timeout: 3000
        //             });
        //         }
        //         else {
        //             Notify.alert(resp.data.message, {
        //                 status: 'warning',
        //                 pos: 'top-center',
        //                 timeout: 3000
        //             });

        //         }
        //         var url = 'api/MstApplicationAdd/GetPANForm60List';
        //         SocketService.get(url).then(function (resp) {
        //             $scope.contactpanform60_list = resp.data.contactpanform60_list;
        //         });
        //         unlockUI();
        //     });
        // }
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
        $scope.documentviewerupload = function (val1, val2) {
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
