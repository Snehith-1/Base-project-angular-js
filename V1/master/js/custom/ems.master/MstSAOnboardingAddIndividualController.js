(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingAddIndividualController', MstSAOnboardingAddIndividualController);

    MstSAOnboardingAddIndividualController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstSAOnboardingAddIndividualController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
    /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingAddIndividualController';
               activate();
        
        function activate() { 
            $scope.input = false
            $scope.span = true
            $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false
            $scope.dateyes = true
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
            var url = 'api/MstSAOnboardingIndividual/TempBureauDocuments';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });

            var url = 'api/MstSAOnboardingInstitution/GetRMNameGid';
            SocketService.get(url).then(function (resp) {
               
                $scope.txtRMGid = resp.data.reportingmanager_gid;
            });

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open4 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open6 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
           
           // SaType = "Individual";
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

           /* var url='api/MstApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
              $scope.document_list = resp.data.application_list;
            });
            var url='api/';
            SocketService.get(url).then(function (resp) {
              $scope.satype_list = resp.data.application_list;
            });
            var url='api/';
            SocketService.get(url).then(function (resp) {
              $scope.saentitytype_list = resp.data.application_list;
            }); */
                  
           
         
           unlockUI();
           $scope.havenotpan = false;
           $scope.havepan = false;
           $scope.view_nopanreasons = false;

          
           var url = 'api/MstSAOnboardingIndividual/GetEntityDefaultID';
           SocketService.get(url).then(function (resp) {
               $scope.cbosaentitytype = resp.data;
           });


        }
        $scope.credit_amountChange = function () {
            var input = document.getElementById('credit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'danger',
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
        $scope.creditscoreChange = function () {
            var input = document.getElementById('bureau_score').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = inWords(str);
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
        //Number in words
        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

        $scope.recproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        //Individual pan validiation
        $scope.PANValidation = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/MstSAOnboardingBussDevtVerification/IndividualPannumbervalidate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                        $scope.txtpan_no = '';

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
                                    $scope.txtsacontact_person_first_name = parts[0];
                                    $scope.txtsacontact_person_middle_name = parts[1];
                                    $scope.txtsacontact_person_last_name = parts[2];
                                } else {
                                    $scope.txtsacontact_person_first_name = parts[0];
                                    $scope.txtsacontact_person_last_name = parts[1];
                                }
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidationind = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                                $scope.txtsacontact_person_first_name = '';
                                $scope.txtsacontact_person_middle_name = '';
                                $scope.txtsacontact_person_last_name = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });
                    }
                });
            }
        }
         // PAN Change

         $scope.change_pan = function (cbopanstatus) {
            if ($scope.cbopanstatus == 'Customer submitting PAN') {
                $scope.havepan = true;
                $scope.havenotpan = false;
                angular.forEach($scope.panabsencereason_list, function (val) {
                    val.checked = false;
                });
                var url = 'api/MstSAOnboardingIndividual/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
                    $scope.contactpanform60_list = '';
                });               
            }
            else if ($scope.cbopanstatus == 'Customer submitting Form 60') {
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

            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
               
            }

            frm.append('project_flag', "Default"); 

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

                    var url = 'api/MstSAOnboardingIndividual/GetPANForm60List';
                    SocketService.get(url).then(function (resp) {
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
                var url = 'api/MstSAOnboardingIndividual/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
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

        // $scope.BankAccValidation = function () {

        //     if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
        //         var params = {
        //             ifsc: $scope.txtifsc_code,
        //             accountNumber: $scope.txtconfirmbankaccount_number
        //         }

        //         var url = 'api/Kyc/BankAccVerification';
        //         lockUI();
        //         SocketService.post(url, params).then(function (resp) {
        //             unlockUI();
        //             if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
        //                 $scope.bankaccvalidation = true;
        //                 $scope.txtaccountholder_name = resp.data.result.accountName;

        //             } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
        //                 $scope.bankaccvalidation = false;
        //                 Notify.alert('Bank Account is not verified..!', 'warning');
        //                 $scope.txtaccountholder_name = '';
        //             } else {
        //                 Notify.alert(resp.data.message, 'warning')
        //             }

        //         });
        //     }
        // }

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
        $scope.saveasdraft_individual = function () {
            var panabsencereasons_checked = false;
            var lssatype_gid = '';
            var lssatype_name = '';
            var lssaentitytype_gid = '';
            var lssaentitytype_name = '';

            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';

            if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                lsassessmentagency_gid = $scope.cbosaassessmentagency
                lsassessmentagency_name = $('#assessmentagency_name :selected').text()
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating;
                lsassessmentagencyrating_name = $('#assessmentagencyrating_name :selected').text()
            }
            //if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}

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

            if ($scope.cbosatype != undefined || $scope.cbosatype != null) {
                lssatype_gid = $scope.cbosatype.satype_gid;
                lssatype_name = $scope.cbosatype.satype_name;
            }
            if ($scope.cbosaentitytype != undefined || $scope.cbosaentitytype != null) {
                lssaentitytype_gid = $scope.cbosaentitytype;
                lssaentitytype_name = $('#saentitytype :selected').text();
             //   lssaentitytype_name = $scope.cbosaentitytype.saentitytype_name;
            }


            //if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
            //    Notify.alert('Kindly Enter PAN Value', 'warning')
            //}
            //else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
            //    Notify.alert('Kindly Upload Form 60 Document', 'warning')
            //}
            //else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
            //    Notify.alert('Kindly Select Reasons for Uploading Form 60 Document', 'warning')
            //}
            


                var params = {

                    //satype_gid: lssatype_gid,
                    satype_gid: 'MSAG202207047',
                    satype_name: 'Individual',
                    sa_reportingmanager: $scope.txtRM,
                    reportingmanager_gid: $scope.txtRMGid,
                    saentitytype_gid: lssaentitytype_gid,
                    //satype_name: lssatype_name,
                    saentitytype_name: lssaentitytype_name,
                    pan_status: $scope.cbopanstatus,
                    sa_pannumber: $scope.txtpan_no,
                    gender: $scope.cbogender,
                    sa_firstname: $scope.txtsacontact_person_first_name,
                    sa_middlename: $scope.txtsacontact_person_middle_name,
                    sa_lastname: $scope.txtsacontact_person_last_name,
                    individual_pannumber: $scope.txtindividual_pannumber,
                    sa_aadharnumber: $scope.txtindividual_aadharnumber,
                    sa_apputr: $scope.txtutr_number,
                    sa_appcrediteddate: $scope.txtcredited_date,
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
                    approvalstatus: 'Draft',
                    panabsencereason_selectedlist: panabsencereason_selectedList,
                    assessmentagency_gid: lsassessmentagency_gid,
                    assessmentagency_name: lsassessmentagency_name,
                    assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                    assessmentagencyrating_name: lsassessmentagencyrating_name,
                    ratingas_date: $scope.txtrasteason_date

                }
                console.log(params);
                var url = 'api/MstSAOnboardingIndividual/OnboardSubmitSaveasdraft';
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
        $scope.submit_individual =function () {
            var panabsencereasons_checked = false;
            var lssatype_gid = '';
            var lssatype_name = '';
            var lssaentitytype_gid = '';
            var lssaentitytype_name = '';

            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';

            if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
                lsassessmentagency_gid = $scope.cbosaassessmentagency
                lsassessmentagency_name = $('#assessmentagency_name :selected').text()
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating;
                lsassessmentagencyrating_name = $('#assessmentagencyrating_name :selected').text()
            }
            //if ($scope.cbosaassessmentagency != undefined || $scope.cbosaassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cbosaassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cbosaassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}

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
             
            if ($scope.cbosatype != undefined || $scope.cbosatype != null) {
                lssatype_gid = $scope.cbosatype.satype_gid;
                lssatype_name = $scope.cbosatype.satype_name;
            }
            if ($scope.cbosaentitytype != undefined || $scope.cbosaentitytype != null) {
                lssaentitytype_gid = $scope.cbosaentitytype;
                lssaentitytype_name = $('#saentitytype :selected').text()
             //   lssaentitytype_name = $scope.cbosaentitytype.saentitytype_name;
            }


            if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
                Notify.alert('Kindly enter PAN value', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
                Notify.alert('Kindly upload Form 60 document', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
                Notify.alert('Kindly select reasons for uploading Form 60 document', 'warning')
            }
            else{
                
               
            var params = {
                   
                //satype_gid: lssatype_gid,
                satype_gid: 'MSAG202207047',
                satype_name: 'Individual',
                    sa_reportingmanager: $scope.txtRM,
                    reportingmanager_gid: $scope.txtRMGid,
                    saentitytype_gid: lssaentitytype_gid,
                    //satype_name: lssatype_name,
                    saentitytype_name: lssaentitytype_name,
                    pan_status: $scope.cbopanstatus,
                    sa_pannumber: $scope.txtpan_no,
                    
                    sa_firstname: $scope.txtsacontact_person_first_name,
                    sa_middlename: $scope.txtsacontact_person_middle_name,
                    sa_lastname: $scope.txtsacontact_person_last_name,
                    gender: $scope.cbogender,
                    individual_pannumber: $scope.txtindividual_pannumber,
                    sa_aadharnumber :$scope.txtindividual_aadharnumber,
                    sa_apputr: $scope.txtutr_number,
                    sa_appcrediteddate: $scope.txtcredited_date,
                    sa_appcreditedamount:$scope.txtcredited_amount,
                    saifsc_code:$scope.txtifsc_code,
                    saaccount_number:$scope.txtbankaccount_number,
                    confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                    saaccountholder_name: $scope.txtaccountholder_name,
                    sacanccheque_number: $scope.txtcancelledcheque_number,
                    sabank_name:$scope.txtbank_name,
                    sabranch_name: $scope.txtbranch_name,
                    city: $scope.txtcity,
                    district: $scope.txtdistrict,
                    state: $scope.txtstate,
                    micr: $scope.txtmicr,
                    branch_address: $scope.txtbranch_address,
                    approvalstatus : 'Pending BD Verification',
                    panabsencereason_selectedlist: panabsencereason_selectedList,
                    assessmentagency_gid: lsassessmentagency_gid,
                    assessmentagency_name: lsassessmentagency_name,
                    assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                    assessmentagencyrating_name: lsassessmentagencyrating_name,
                    ratingas_date: $scope.txtrasteason_date

            }
            console.log(params);
            var url = 'api/MstSAOnboardingIndividual/OnboardSubmit';
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
            $state.go('app.MstSAOnboardingIndividualSummary');
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
        //$scope.add_mobileno = function () {
        //    if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
        //        Notify.alert('Enter Mobile Number/Select Status', 'warning');
        //    }
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter mobile number/select status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit mobile number', 'warning');
            }
            else {
                 var params = {
                    samobile_no: $scope.txtmobile_no,
                    saprimary_status: $scope.rdbprimarymobile_no,
                    sawhatsapp_no: $scope.rdbwhatsappmobile_no
                }
                var url = 'api/MstSAOnboardingIndividual/MobileNumberAdd';
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
            var url = 'api/MstSAOnboardingIndividual/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.individualmobileno_list = resp.data.Sacontactmobileno_list;
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
            frm.append('project_flag', "Default"); 
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstSAOnboardingIndividual/SaInstitutionDocumentUpload';
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
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var param = {
                        saindividual2bureau_gid: $scope.saindividual2bureau_gid
                    };
                    var url = 'api/MstSAOnboardingIndividual/SAUploadIndividualDocList';
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

        $scope.uploaddocumentcancel = function (individualsabureaudocumentupload_gid) {
            lockUI();
            var params = {
                individualsabureaudocumentupload_gid: individualsabureaudocumentupload_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteBureauDocuments';
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
                    saindividual2bureau_gid: $scope.saindividual2bureau_gid
                };
                var url = 'api/MstSAOnboardingIndividual/SAUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lrfilename = resp.data.filename;
                    $scope.lrfilepath = resp.data.filepath;
                    $scope.cicuploaddoc_list = resp.data.sauploaddoc_list;
                });
                unlockUI();
            });
        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.report_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.addbureau_individual = function () {
            //if (($scope.cboBureauName == undefined || $scope.cboBureauName == null || $scope.cboBureauName == '') || ($scope.txtbureauscore_date == null || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
            //    ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtbureauscore_date == undefined || $scope.txtbureauscore_date == '') ||
            //    ($scope.txtbureau_score == null || $scope.txtbureau_score == undefined || $scope.txtbureau_score == '') || ($scope.txtobservations == null || $scope.txtobservations == undefined || $scope.txtobservations == '') || ($scope.txtbureau_response == '' || $scope.txtbureau_response == undefined || $scope.txtbureau_response == null)) {

            //    Notify.alert('Enter All Mandatory Values', 'warning');
            //}
            //else {
               
                var params = {
                    
                    sacontact_gid: $scope.sacontact_gid,
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,
                }
                var url = 'api/MstSAOnboardingIndividual/PostSABureauInstitution';
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
                        sacontact_gid: $scope.sacontact_gid,
                    }
                    var url = 'api/MstSAOnboardingIndividual/GetSABureauIndividualTempList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.contactbureau_list = resp.data.sainstitutebureau_list;

                    });



                });
            //}
        }

        //$scope.bureau_view = function (saindividual2bureau_gid) {
        //    $location.url('app/MstSAonboardingIndBureauView?lssaindividual2bureau_gid=' + saindividual2bureau_gid + '&lssacontact_gid=' + $scope.sacontact_gid);
        //}

        $scope.bureau_view = function (saindividual2bureau_gid) {
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
                    saindividual2bureau_gid: saindividual2bureau_gid
                }

                var url = 'api/MstSAOnboardingIndividual/SABureauView';

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

                var url = 'api/MstSAOnboardingIndividual/SAUploadIndDocList';
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

            }
        }
        $scope.bureau_delete = function (saindividual2bureau_gid) {
            var params = {
                saindividual2bureau_gid: saindividual2bureau_gid
            }
            var url = 'api/MstSAOnboardingIndividual/DeleteContactBureau';
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
                
//Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter email address/select status');
            }
            else {
                var params = {
                    saemail_address: $scope.txtemail_address,
                    saprimary_status: $scope.rdbprimaryemail_address,
                }
                var url = 'api/MstSAOnboardingIndividual/PostEmailAddress';
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
            var url = 'api/MstSAOnboardingIndividual/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardemailaddress_list = resp.data.saOnboardemailaddress_list;
            }); 
        }
//Prospects Multiple Add
        $scope.add_prospects = function () {
            if (($scope.txtlead_name == undefined) || ($scope.txtlead_name == '') || ($scope.txtsector_name == undefined) || ($scope.txtsector_name == '')) {
                Notify.alert('Enter lead name/sector','warning');
            }
            else 
            {
            var params={
                salead_name : $scope.txtlead_name,
                sasector_industry : $scope.txtsector_name
            }
           
            var url = 'api/MstSAOnboardingIndividual/AddProspects';
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
            var url = 'api/MstSAOnboardingIndividual/GetProspectsList';
            SocketService.get(url).then(function (resp) {
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
                backdrop: 'static',
                keyboard: false,
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
                        $scope.txtcountry = resp.data.country;
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
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/MstSAOnboardingIndividual/PostAddress';
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
            var url = 'api/MstSAOnboardingIndividual/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.saOnboardaddress_list;
            }); 
        }

        //Document Multiple Add
        $scope.saindivdualdoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
}

        $scope.individualDocumentUpload = function (val, val1, name) {
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
                frm.append('project_flag', "Default"); 

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstSAOnboardingIndividual/AddDocuments';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.lufilename = resp.data.filename;
                        $scope.lufilepath = resp.data.filepath;
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
                            var url = 'api/MstSAOnboardingIndividual/GetDocumentsList';
                                    SocketService.get(url).then(function (resp) {
                                        $scope.lufilename = resp.data.filename;
                                        $scope.lufilepath = resp.data.filepath;
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
                        $scope.txtindivual_document = "";
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
                var url = 'api/MstSAOnboardingIndividual/GetDocumentsList';
                SocketService.get(url).then(function (resp) {
                    $scope.lufilename = resp.data.filename;
                    $scope.lufilepath = resp.data.filepath;
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

     /*   ///aadhar XXXX validation
 const phoneNoRef = document.querySelector('#cardNumber');
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
//
       /* $scope.txtlastyear_turnoverchange = function () {

            var num = document.getElementById('cardNumber').value;
            var react=num.replace(/(\d{4})(\d{4})(\d{4})/, '$1-$2-$3'); 
//$(".numbers").each(function() {
            //    var text = $(this).text();
            var newText=num.replace(/(\d{4})(\d{4})(\d{4})/, 'XXXX-XXXX-$3')
               // var newText = react.replace(/\d{1}/g, "X");
               // $(this).text(newText);
          //  }
           // var firstDigits = react(0, 4);
           // var lastDigits = input(input.Length - 4, 4);
           // var requiredMask = new String('X', react.Length);
          //  var maskedString = Concat(requiredMask);//, requiredMask, lastDigits);
           // var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");

            {
                $scope.txtindividual_aadharnumber = newText;
                document.getElementById('words_totalamount').innerHTML =react;
            }
        }
        ///*/
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
        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/MstSAOnboardingIndividual/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
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
