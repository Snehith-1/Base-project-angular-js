(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingAddInstitutionController', MstSAOnboardingAddInstitutionController);

    MstSAOnboardingAddInstitutionController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstSAOnboardingAddInstitutionController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
    /* jshint validthis:true */
        var vm = this;
        activate();

        function activate() {  
            
            $scope.input = false
            $scope.span = true
            $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false
            $scope.dateyes = true
            var url = 'api/MstSAOnboardingInstitution/TempDeleteMobileNo';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempEmailAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempAddress';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempGST';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempIndividual';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempProspects';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempDocuments';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstSAOnboardingInstitution/TempBureauDocuments';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });
          vm.formats = ['dd-MM-yyyy'];
          vm.format = vm.formats[0];

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
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open5 = true;
            };
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open6 = true;
            };
            vm.calender7= function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open7 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            }; 
            lockUI();
            var url='api/customer/state';
            SocketService.get(url).then(function (resp) {
              $scope.satype_list = resp.data.application_list;
            });
            var url='api/customer/state';
            SocketService.get(url).then(function (resp) {
              $scope.saentitytype_list = resp.data.application_list;
            }); 
            var url='api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
              });
            
              var url = 'api/MstSAOnboardingInstitution/GetRMName';
              SocketService.get(url).then(function (resp) {
                  unlockUI();
                  
                  $scope.txtRM = resp.data.reporting_manager;
              });
              var url = 'api/MstSAOnboardingInstitution/GetRMNameGid';
              SocketService.get(url).then(function (resp) {
                  unlockUI();

                  $scope.txtRMGid = resp.data.reportingmanager_gid;
              });
              var params = {

                  satype_gid: $scope.cbosatype,
                  saentitytype_gid: $scope.cbosaentitytype,
                  designation_gid: $scope.cboDesignation,                          
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

              SocketService.getparams(url, params).then(function (resp) {
                $scope.designationlist = resp.data.sadesignationlist;
                });

              SocketService.get(url).then(function (resp) {
                  $scope.saassessmentagencylist = resp.data.saassessmentagencylist;
              });

              SocketService.get(url).then(function (resp) {
                  $scope.assessmentagencyratinglist = resp.data.assessmentagencyratinglist;
              });

              var url = 'api/MstApplicationAdd/PANAbsenceReasonList';
              SocketService.get(url).then(function (resp) {
                  $scope.panabsencereason_list = resp.data.panabsencereason_list;
              });

           unlockUI();
        }

        $scope.onselected_yes = function () {
            if ($scope.rdbgstregister_status == 'Yes') {
                $scope.rdbgstregister_status = true;
                $scope.rdbgstregister_status = 'Yes';
               
            }
            else {
                $scope.rdbgstregister_status = false;
                $scope.rdbgstregister_status = 'No';
            }            
        }

        //Individual pan validiation
        $scope.PANValidation = function () {
            if ($scope.txtsa_panno.length == 10) {
                var params = {
                    pan: $scope.txtsa_panno
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionPannumbervalidate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                        $scope.txtsa_panno = '';

                    }
                    else {

                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidationind = true;
                                var parts = resp.data.result.name.split(" ");
                                if (parts.length == 3) {
                                    $scope.txtfirst_name = parts[0];
                                    $scope.txtmiddle_name = parts[1];
                                    $scope.txtlast_name = parts[2];
                                } else {
                                    $scope.txtfirst_name = parts[0];
                                    $scope.txtlast_name = parts[1];
                                }
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidationind = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                $scope.txtfirst_name = '';
                                $scope.txtmiddle_name = '';
                                $scope.txtlast_name = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });
                    }
                });
            }
        }

        $scope.show = function () {

            if ($scope.inputType == 'password')
                $scope.inputType = 'text';
            $scope.eye = false
            $scope.eyeslash = true
            //else
            //    $scope.inputType = 'password';

        }

        $scope.noshow = function () {

            if ($scope.inputType == 'text')
                $scope.inputType = 'password';
            //else
            //    $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false

        }
        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_registration_number;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstSAOnboardingInstitution/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gststate_name;
            });
        }


        $scope.getPANbasedGST = function () {
            if ($scope.txtsa_pannumber.length == 10) {
                if ($scope.gst_Onboard_list != null) {
                    var paramsdel =
                    {
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid
                    }

                    var url = 'api/MstSAOnboardingInstitution/DeleteGSTInstitution';
                    lockUI();
                    SocketService.getparams(url, paramsdel).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                        }
                        else {
                            Notify.alert('Error occured while deleting the existing GST Details..!', 'warning');
                        }
                    });
                }
                var params = {
                    pan: $scope.txtsa_pannumber
                }

                var url = 'api/MstSAOnboardingBussDevtVerification/InstitutionPannumbervalidate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                        $scope.txtpannumber = '';

                    }
                    else {


                        var url = 'api/Kyc/GSTSBPAN';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.statusCode == 101) {
                                $scope.panvalidation = true;
                                const GstArray = resp.data.result;

                                var params = {
                                    GSTArray: GstArray
                                }

                                var url = 'api/MstSAOnboardingInstitution/PostGST';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        institutiongstlist();
                                    }
                                    else {
                                        Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                                    }

                                });

                            } else if (resp.data.statusCode == 103) {
                                var param = {
                                    pan: $scope.txtsa_pannumber
                                }
                                var url = 'api/Kyc/PANNumber';
                                lockUI();
                                SocketService.post(url, param).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                        $scope.panvalidation = true;
                                        institutiongstlist();
                                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                        $scope.panvalidation = false;
                                        Notify.alert('PAN is not verified..!', 'warning');
                                        institutiongstlist();
                                    } else {
                                        Notify.alert(resp.data.message, 'warning')
                                    }

                                });

                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                });
                        
            }
        }

        function institutiongstlist() {
            var url = 'api/MstSAOnboardingInstitution/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.gst_Onboard_list = resp.data.gst_Onboard_list;
            });
        }
      
        $scope.submit_company = function () {
            var lssatype_gid = '';
            var lssatype_name = '';
            var lssaentitytype_gid = '';
            var lssaentitytype_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';



            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_type = $scope.cboDesignation.designation_type;
            }
            if ($scope.cbosatype != undefined || $scope.cbosatype != null) {
                lssatype_gid = $scope.cbosatype.satype_gid;
                lssatype_name = $scope.cbosatype.satype_name;
            }
            if ($scope.cbosaentitytype != undefined || $scope.cbosaentitytype != null) {
                lssaentitytype_gid = $scope.cbosaentitytype.saentitytype_gid;
                lssaentitytype_name = $scope.cbosaentitytype.saentitytype_name;
            }

         

            if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            var params = {
                satype_gid: 'MSAG202206294',
                satype_name: 'Company',
                //satype_gid: lssatype_gid,
                sa_reportingmanager: $scope.txtRM,
                reportingmanager_gid: $scope.txtRMGid,
               // satype_name: lssatype_name,
                saentitytype_name: lssaentitytype_name,
                saentitytype_gid: lssaentitytype_gid,
                // sa_designation: $scope.cbodesignation,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                sa_associatename: $scope.txtsamunnati_associate_name,
                sa_contactfirstname: $scope.txtsacontact_person_first_name,
                sa_contactmiddlename: $scope.txtsacontact_person_middle_name,
                sa_contactlastname: $scope.txtsacontact_person_last_name,
                sa_dateofincorporation: $scope.txtdateofincorporation_date,
                sa_annualturnover: $scope.txtannual_turnover,
                sa_companypan: $scope.txtsa_pannumber,
                sa_companystdate: $scope.txtcompanystart_date,
                sa_yearsinbusiness: $scope.txtyearin_business,
                sa_monthsinbusiness: $scope.txtmonthsin_business,
                // pan_number: $scope.txtpan_number,
                sa_startdate: $scope.txtstart_date,
                sa_enddate: $scope.txtend_date,
                saifsc_code: $scope.txtifsc_code,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                saaccount_number: $scope.txtbankaccount_number,
                confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                saaccountholder_name: $scope.txtaccountholder_name,
                sacanccheque_number: $scope.txtcancelledcheque_number,
                sabank_name: $scope.txtbank_name,
                sabranch_name: $scope.txtbranch_name,
                approvalstatus: 'Pending BD Verification',
                sa_apputr: $scope.txtutr_number,
                sa_appcrediteddate: $scope.txtcredited_date,
                sa_appcreditedamount: $scope.txtcredited_amount,

                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                ratingas_date: $scope.txtrasteason_date,
                rdbgstregister_status: $scope.rdbgstregister_status,

            }
            console.log(params);
            var url = 'api/MstSAOnboardingInstitution/OnboardSubmit';

            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstSAOnboardingSummary');
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
        $scope.saveasdraft_company = function () {
            var lssatype_gid = '';
            var lssatype_name = '';
            var lssaentitytype_gid = '';
            var lssaentitytype_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';



            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_type = $scope.cboDesignation.designation_type;
            }
            if ($scope.cbosatype != undefined || $scope.cbosatype != null) {
                lssatype_gid = $scope.cbosatype.satype_gid;
                lssatype_name = $scope.cbosatype.satype_name;
            }
            if ($scope.cbosaentitytype != undefined || $scope.cbosaentitytype != null) {
                lssaentitytype_gid = $scope.cbosaentitytype.saentitytype_gid;
                lssaentitytype_name = $scope.cbosaentitytype.saentitytype_name;
            }



            if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            var params = {
                satype_gid: 'MSAG202206294',
                satype_name: 'Company',
                //satype_gid: lssatype_gid,
                sa_reportingmanager: $scope.txtRM,
                reportingmanager_gid: $scope.txtRMGid,
                // satype_name: lssatype_name,
                saentitytype_name: lssaentitytype_name,
                saentitytype_gid: lssaentitytype_gid,
                // sa_designation: $scope.cbodesignation,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                sa_associatename: $scope.txtsamunnati_associate_name,
                sa_contactfirstname: $scope.txtsacontact_person_first_name,
                sa_contactmiddlename: $scope.txtsacontact_person_middle_name,
                sa_contactlastname: $scope.txtsacontact_person_last_name,
                sa_dateofincorporation: $scope.txtdateofincorporation_date,
                sa_annualturnover: $scope.txtannual_turnover,
                sa_companypan: $scope.txtsa_pannumber,
                sa_companystdate: $scope.txtcompanystart_date,
                sa_yearsinbusiness: $scope.txtyearin_business,
                sa_monthsinbusiness: $scope.txtmonthsin_business,
                // pan_number: $scope.txtpan_number,
                sa_startdate: $scope.txtstart_date,
                sa_enddate: $scope.txtend_date,
                saifsc_code: $scope.txtifsc_code,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                saaccount_number: $scope.txtbankaccount_number,
                confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                saaccountholder_name: $scope.txtaccountholder_name,
                sacanccheque_number: $scope.txtcancelledcheque_number,
                sabank_name: $scope.txtbank_name,
                sabranch_name: $scope.txtbranch_name,
                approvalstatus: 'Draft',
                sa_apputr: $scope.txtutr_number,
                sa_appcrediteddate: $scope.txtcredited_date,
                sa_appcreditedamount: $scope.txtcredited_amount,

                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                ratingas_date: $scope.txtrasteason_date



            }
            console.log(params);
            var url = 'api/MstSAOnboardingInstitution/OnboardSubmitSaveasdraft';

            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstSAOnboardingSummary');
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

        $scope.save_company = function () {
            if (($scope.cbosaentitytype == '') || ($scope.cbosaentitytype == undefined) || ($scope.cbosatype == '') || ($scope.cbosatype == undefined)) {
                Notify.alert('Please fill mandatory fields');
            }
            else {
                $scope.mandatoryfields = false;
                var designation = $('#designation_type :selected').text();

                var params = {
                    satype_gid: $scope.cbosatype,
                    sa_reportingmanager: $scope.txtRM,
                    saentitytype_gid: $scope.cbosaentitytype,
                    sa_designation: $scope.cboDesignation,
                    //  sa_designation: designation,
                    sa_associatename: $scope.txtsamunnati_associate_name,
                    sa_contactfirstname: $scope.txtsacontact_person_first_name,
                    sa_contactmiddlename: $scope.txtsacontact_person_middle_name,
                    sa_contactlastname: $scope.txtsacontact_person_last_name,
                    sa_dateofincorporation: $scope.txtdateofincorporation_date,
                    sa_annualturnover: $scope.txtannual_turnover,
                    sa_companypan: $scope.txtsa_pannumber,
                    sa_companystdate: $scope.txtcompanystart_date,
                    sa_yearsinbusiness: $scope.txtyearin_business,
                    sa_monthsinbusiness: $scope.txtmonthsin_business,
                    // pan_number: $scope.txtpan_number,
                    sa_startdate: $scope.txtstart_date,
                    sa_enddate: $scope.txtend_date,
                    saifsc_code: $scope.txtifsc_code,
                    saaccount_number: $scope.txtbankaccount_number,
                    confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                    saaccountholder_name: $scope.txtaccountholder_name,
                    sacanccheque_number: $scope.txtcancelledcheque_number,
                    sabank_name: $scope.txtbank_name,
                    sabranch_name: $scope.txtbranch_name
                }
                console.log(params);
                var url = 'api/MstSAOnboardingInstitution/OnboardSave';
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
                    $state.go('app.MstSAOnboardingSummary');
                });
            }

        }
       
        $scope.back = function () {
            $state.go('app.MstSAOnboardingSummary');
        }


        //Bank Details
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
       /* const phoneNoRef = document.querySelector('#cardNumber');
        phoneNoRef.addEventListener('keypress', (evt) => {
          const code = evt.keyCode;
          const val = evt.currentTarget.value;
          const len = val.length;
          //console.log(len);
          //allow only numbers and backspace.
          //hyphen will be added automatically
          //user cannot enter hyphen
          if(!((code >= 48 && code <= 57) || code === 12)) {
              evt.preventDefault();
          }
          if(len === 4 || len === 9) {
              console.log('add a hyphen');
              evt.currentTarget.value += '-'
          }
          if(len === 14) {
              console.log('do not allow any more');
              evt.preventDefault();
          }
        });
*/
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

        $scope.CICDocumentUpload = function (val, val1, name) {

            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "Default"); 
            $scope.uploadfrm = frm;
           
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingInstitution/SaInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lrfilename = resp.data.filename;
                        $scope.lrfilepath = resp.data.filepath;
                        $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.uploaddocumentcancel = function (institutionsabureaudocumentupload_gid) {
            lockUI();
            var params = {
                institutionsabureaudocumentupload_gid: institutionsabureaudocumentupload_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteBureauDocuments';
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                var param = {
                    sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                };
                var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }



//Mobile Number Multiple Add
            $scope.add_mobileno = function () {
                if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_no == undefined) || ($scope.rdbwhatsapp_no == '') || ($scope.rdbprimary_status == '')) {
                    Notify.alert('Enter mobile No/select primary status and whatsapp number', 'warning');
                }
                else if ($scope.txtmobile_no.length < 10) {
                    Notify.alert('Enter 10 digit mobile number', 'warning');
                }
                else {
                    var params = {
                        samobile_no: $scope.txtmobile_no,
                        saprimary_status: $scope.rdbprimary_status,
                        sawhatsapp_no: $scope.rdbwhatsapp_no
                    }
                    var url = 'api/MstSAOnboardingInstitution/MobileNumberAdd';
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
                    

                        var url = 'api/MstSAOnboardingInstitution/GetMobileNoList';
                        SocketService.get(url).then(function (resp) {
                            $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;

                        });

                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_status = '';
                        $scope.rdbwhatsapp_no = '';
                        $scope.rdbprimary_no == false;
                    });
              
                }
            }
        $scope.delete_mobileno = function (sainstitution2mobileno_gid) {
            var params =
                {
                    sainstitution2mobileno_gid: sainstitution2mobileno_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteMobileNo';
            
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
            var url = 'api/MstSAOnboardingInstitution/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.SacontactInstimobileno_list = resp.data.SacontactInstimobileno_list;
            }); 
        }
//Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbemail_type == undefined) || ($scope.rdbemail_type == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter email address/select status','warning');
            }
            else {
                var params = {
                    saemail_address: $scope.txtemail_address,
                    samail_type: $scope.rdbemail_type,
                    saprimary_status: $scope.rdbprimaryemail_address,
                }
                var url = 'api/MstSAOnboardingInstitution/PostEmailAddress';
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
                    $scope.rdbemail_type = '';
                    $scope.rdbprimaryemail_address == false;
                }); 
            }
        }
        $scope.delete_emailaddress = function (sainstitution2email_gid) {
            var params =
                {
                    sainstitution2email_gid: sainstitution2email_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteEmailAddress';
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
            var url = 'api/MstSAOnboardingInstitution/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardInstiemailaddress_list = resp.data.saOnboardInstiemailaddress_list;
            }); 
        }
//Company Year and Month
        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtcompanystart_date
            }
            var url = 'api/Mstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
            });
        }

        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtannual_turnover = "";
            }
            else {
                $scope.txtannual_turnover = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }
        $scope.creditscoreChange = function () {
            var input = document.getElementById('bureau_score').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtbureau_score = "";
            }
            else {
                $scope.txtbureau_score = output;
            }
        }
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcredited_amount = "";
            }
            else {
                $scope.txtcredited_amount = output;
                document.getElementById('words_creditamt').innerHTML = lswords_creditamount;
            }
        }
//GST Multiple Add
        $scope.gst_add = function () {

            if (($scope.rdbgstregister_status == undefined) || ($scope.rdbgstregister_status == '') || ($scope.txtgst_state == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_registration_number == undefined) || ($scope.txtgst_registration_number == '')) {

                Notify.alert('Enter GST state / select GST registered status / GST number', 'warning');
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
            
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_registration_number,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/MstSAOnboardingInstitution/PostInstitutionGST';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    gst_list();                  
                    $scope.txtgst_state = '';
                    $scope.txtgst_registration_number = '';
                    $scope.rdbgstregister_status = '';
                  
                   

                });
            }
        }

        $scope.gst_delete = function (sainstitution2gst_gid) {
            var params =
                {
                    sainstitution2gst_gid: sainstitution2gst_gid
                }
            console.log(params)
            var url = 'api/MstSAOnboardingInstitution/DeleteGST';
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/MstSAOnboardingInstitution/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.gst_Onboard_list = resp.data.gst_Onboard_list;

            });
        }


//Address Multiple Add
        $scope.add_address = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.onchangebusinessstartdate = function () {
                    var params = {
                        businessstart_date: $scope.txtbusinessstart_date
                    }
                /* var url = 'api/';*/        
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtyear_business = resp.data.year_business;
                        $scope.txtmonth_business = resp.data.month_business;
                    });
                }
                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/MstSAOnboardingInstitution/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.sacity;
                        $scope.txttaluka = resp.data.sataluka;
                        $scope.txtdistrict = resp.data.sadistrict;
                        $scope.txtstate = resp.data.sastate;
                        $scope.txtcountry = resp.data.sacountry;
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
              
                $scope.addressSubmit = function () {
                    var params = {
                        saaddresstype_gid: $scope.cboaddresstype.address_gid,
                        saaddresstype_name: $scope.cboaddresstype.address_type,
                        saprimary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        salandmark: $scope.txtlandmark,
                        sapostal_code: $scope.txtpostal_code,
                        sacity: $scope.txtcity,
                        sataluka: $scope.txttaluka,
                        sadistrict: $scope.txtdistrict,
                        sastate: $scope.txtstate,
                        sacountry: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude

                    }
                    var url = 'api/MstSAOnboardingInstitution/PostAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } 
                    });
                    $modalInstance.close('closed');
                }
            }
        }
        $scope.delete_address = function (sainstitution2address_gid) {
            var params =
                {
                    sainstitution2address_gid: sainstitution2address_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteAddress';
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
            var url = 'api/MstSAOnboardingInstitution/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardInstiaddress_list = resp.data.saOnboardInstiaddress_list;
            }); 
        }
        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, sapostal_code) {
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
                    var addressString = ''.concat(addressline1.toString(), ",", sapostal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", sapostal_code.toString());
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
        $scope.addbureau_institution = function () {    
             var params = {                    
                    sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,
                }
                var url = 'api/MstSAOnboardingInstitution/PostSABureauInstitution';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.cboBureauName = '';
                        $scope.txtbureauscore_date = '';
                        $scope.txtobservations = '';
                        $scope.txtbureau_response = '';
                        $scope.txtbureau_score = '';
                        $scope.cicuploaddoc_list = '';
                        
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                    var params = {
                        sacontactinstitution_gid: $scope.sacontactinstitution_gid,
                    }
                    var url = 'api/MstSAOnboardingInstitution/GetSABureauInstitutionTempList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.contactbureau_list = resp.data.sainstitutebureau_list;

                    });



                });
            //}
        }

        //$scope.bureau_view = function (sainstitution2bureau_gid) {
        //    $location.url('app/MstSAonboardingBureauView?lssainstitution2bureau_gid=' + sainstitution2bureau_gid + '&lssacontactinstitution_gid=' + $scope.sacontactinstitution_gid);
        //}


        $scope.bureau_view = function (sainstitution2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/bureau_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var param = {
                    sainstitution2bureau_gid: sainstitution2bureau_gid
                }

                var url = 'api/MstSAOnboardingInstitution/SABureauView';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;                  
                    unlockUI();
                });

                var url = 'api/MstSAOnboardingInstitution/SAUploadIndDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadallbureau = function (val1, val2) {

                    for (var i = 0; i < val2.length; i++) {
                        //  console.log(array[i]);
                        DownloaddocumentService.Downloaddocument(val1, val2[i]);
                    }

                }

                $scope.recproof_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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

            }
        }

        $scope.uprep_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.bureau_delete = function (sainstitution2bureau_gid) {
            var params = {
                sainstitution2bureau_gid: sainstitution2bureau_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteContactBureau';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }

            });
        }
        $scope.CICDocumentUpload = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly"); 
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingInstitution/SaInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    //$scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.lrfilename = resp.data.filename;
                        $scope.lrfilepath = resp.data.filepath;
                        $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.uploaddocumentcancel = function (institutionsabureaudocumentupload_gid) {
            lockUI();
            var params = {
                institutionsabureaudocumentupload_gid: institutionsabureaudocumentupload_gid
            }
            var url = 'api/MstSAOnboardingInstitution/DeleteBureauDocuments';
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                var param = {
                    sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                };
                var url = 'api/MstSAOnboardingInstitution/SAUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

//Prospects Multiple Add
        $scope.add_prospects = function () {
            if (($scope.txtlead_name == '') || ($scope.txtlead_name == undefined) || ($scope.txtsector_name == undefined) || ($scope.txtsector_name == '')) {
                Notify.alert('Enter all mandatory values','warning');
            }
            else {
            var params={
                salead_name: $scope.txtlead_name,
                sasector_industry: $scope.txtsector_name
            }
            var url = 'api/MstSAOnboardingInstitution/AddIndividualProspects';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                 prospects_list();
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
            prospects_list();
            $scope.txtlead_name = '',
            $scope.txtsector_name = ''
        }
        function prospects_list() {
            var url = 'api/MstSAOnboardingInstitution/GetProspectsList';
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardInstiProspects_list = resp.data.saOnboardInstiProspects_list;
            });
        }
        $scope.delete_prospects = function (saprospects_institution_gid) {
            var params =
                {
                    saprospects_institution_gid: saprospects_institution_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteProspects';
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
//Individual Multiple Add
$scope.add_individual = function () {
    if (($scope.txtfirst_name == '') || ($scope.txtfirst_name == undefined) || ($scope.cboindividualdesignation == undefined) || ($scope.txtlast_name == '') || ($scope.txtlast_name == undefined) || ($scope.txtsa_panno == '') || ($scope.txtsa_panno == undefined) || ($scope.txtaadhar_number == '') || ($scope.txtaadhar_number == undefined)) {
        Notify.alert('Enter all mandatory values','warning');
    }
    else {
    var params={
        sa_firstname: $scope.txtfirst_name,
        sa_middlename: $scope.txtmiddle_name,
        sa_lastname: $scope.txtlast_name,
        sa_designation: $scope.cboindividualdesignation,
        sa_pannumber: $scope.txtsa_panno,
        sa_aadharnumber: $scope.txtaadhar_number
    }

         var url='api/MstSAOnboardingInstitution/PostIndividualDetails'
    lockUI();
    SocketService.post(url, params).then(function (resp) {
    unlockUI();
    if (resp.data.status == true) {
        Notify.alert(resp.data.message, {
            status: 'success',
            pos: 'top-center',
            timeout: 3000
        });
        individual_list();
    }
    else {
        Notify.alert(resp.data.message, {
            status: 'warning',
            pos: 'top-center',
            timeout: 3000
        });
    }   
    });
    individual_list();
    }
    
    $scope.txtfirst_name = '',
    $scope.txtmiddle_name = '',
    $scope.txtlast_name = '',
    $scope.cboindividualdesignation = '',
    $scope.txtsa_panno = '',
    $scope.txtaadhar_number = '',
    $scope.panvalidationind = false;
}
        function individual_list() {
            var url = 'api/MstSAOnboardingInstitution/GetIndividualList'
            SocketService.get(url).then(function (resp) {
                $scope.onboard_IndividualInsti_list = resp.data.onboard_IndividualInsti_list;
            });
        }
        $scope.delete_individual = function (sainst_individual_gid) {
            var params =
                {
                    sainst_individual_gid: sainst_individual_gid
                }
            var url = 'api/MstSAOnboardingInstitution/DeleteIndividual'
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
                individual_list();
            }); 
        }

        // PAN Change

        $scope.change_pan = function (cbopanstatus) {
            if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                $scope.havepan = true;
                $scope.havenotpan = false;
                angular.forEach($scope.panabsencereason_list, function (val) {
                    val.checked = false;
                });
                var url = 'api/MstApplicationAdd/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
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

        $scope.downloadall1 = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        
        }
                
//Document Multiple Add
        $scope.companydoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.UploadcompanyDocument = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
                $("#companyfile").val('');
                Notify.alert('Kindly enter the document title/ID', 'warning');
            }
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
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
                frm.append('project_flag', "documentformatonly"); 

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstSAOnboardingInstitution/InstitutionDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
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
                            var url = 'api/MstSAOnboardingInstitution/GetUploadDocumentsList';
                                    SocketService.get(url).then(function (resp) {
                                        $scope.lufilename = resp.data.filename;
                                        $scope.lufilepath = resp.data.filepath;
                                        $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
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
        $scope.delete_companydocument = function (sainstidocument_gid) {
            lockUI();
            var params = {
                sainstidocument_gid: sainstidocument_gid
            }
            var url = 'api/MstSAOnboardingInstitution/UploadDocumentsDelete';
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
                var param = {
                    sainstitution2bureau_gid: $scope.sainstitution2bureau_gid
                };
                var url = 'api/MstSAOnboardingInstitution/GetUploadDocumentsList';
                SocketService.get(url).then(function (resp) {
                    $scope.lufilename = resp.data.filename;
                    $scope.lufilepath = resp.data.filepath;
                    $scope.saOnboardInstiDocument_list = resp.data.saOnboardInstiDocument_list;
                });
                unlockUI();
            });
        }

      
        //Cancel Cheque
        $scope.UploadDocument = function (val, val1, name) {
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly"); 
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstSAOnboardingInstitution/SaInstCancelChequeUpload';
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
            var url = 'api/MstSAOnboardingInstitution/GetSaChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.sachequedocument_list = resp.data.sachequedocument_list;
            });
        }

        $scope.delete_document = function (institutioncancelchequeupload_gid) {
            lockUI();
            var params = {
                institutioncancelchequeupload_gid: institutioncancelchequeupload_gid
            }
            var url = 'api/MstSAOnboardingInstitution/ChequeDocumentDelete';
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

        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/MstSAOnboardingInstitution/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                    $scope.txtcredited_date = '';
                }
            });
        }
        $scope.documentinsviewer = function (val1, val2) {
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