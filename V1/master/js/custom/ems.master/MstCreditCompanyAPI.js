(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditCompanyAPIController', MstCreditCompanyAPIController);

    MstCreditCompanyAPIController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstCreditCompanyAPIController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditCompanyAPIController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid; 
        $scope.lsapi_name = $location.search().lsapi_name;
        var lsapi_name = $scope.lsapi_name;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        lockUI();
        activate();
        function activate() {
           
            var params = {
                institution_gid: institution_gid
            }
            $scope.lsapi_name = $location.search().lsapi_name;
            var url = 'api/MstAPIVerifications/GetStateList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mstgst_list = resp.data.mstgst_list;
            });
            var url = 'api/MstAPIVerifications/GetGSTList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutiongst_list = resp.data.mstgst_list;
            });
            var url = 'api/ProbeAPI/GetInstitutionDetails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.probecompany_name = resp.data.company_name;
                $scope.probecompanypan_no = resp.data.companypan_no;
                $scope.probecin_no = resp.data.cin_no;
            });
            var url = 'api/ProbeAPI/InstitutionProbeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionprobe_list = resp.data.institutionprobe_list;
            });
            var url = 'api/ProbeAPI/InstitutionProbeDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionprobedoc_list = resp.data.institutionprobedoc_list;
            });
            var url = 'api/MstAPIVerifications/GetTAN';
            var params = {
                function_gid: institution_gid,
            }
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tan_list = resp.data.tan_list;
            });
            var url = 'api/MstAPIVerifications/GetMCASignature';
            var params = {
                function_gid: institution_gid,
            }           
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mcasign_list = resp.data.cin_list;
            });
            var url = 'api/MstAPIVerifications/GetCompanyLLP';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cin_list = resp.data.cin_list;
            });
            var url = 'api/MstAPIVerifications/GetIECDetailed';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.IECDetailed_list = resp.data.IECDetailed_list;
            });
            var url = 'api/MstAPIVerifications/GetFDA';
          
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.fda_list = resp.data.fda_list;

            });
            var url = 'api/MstAPIVerifications/GetFSSAI';
           
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.fssai_list = resp.data.fssai_list;

            });
            var url = 'api/MstAPIVerifications/GetLPGIDList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.LPGID_list = resp.data.LPGID_list;
            });
            var url = 'api/MstAPIVerifications/GetShopList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.shop_list = resp.data.shop_list;
            });
            var url = 'api/MstAPIVerifications/GetRCAuthAdvancedList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
            });
            var url = 'api/MstAPIVerifications/GetRCSearchList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RCSearch_list = resp.data.RCSearch_list;
            });
            var url = 'api/MstAPIVerifications/GetPropertyTaxList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.PropertyTax_list = resp.data.PropertyTax_list;
            });   
            
            var paramcrime = {
                institution_gid: institution_gid
            }
            var url = 'api/CrimeCheckAPI/GetCrimeRecordCompanyDetail';
            SocketService.getparams(url, paramcrime).then(function (resp) {
                unlockUI();
                $scope.company_name = resp.data.company_name;               
                $scope.company_cin = resp.data.company_cin;               
                $scope.companyaddress_list = resp.data.companyaddress_list;         
            });  
            var url = 'api/CrimeCheckAPI/GetCompanyReportList';
            SocketService.getparams(url, paramcrime).then(function (resp) {
                unlockUI();
                $scope.companycrimereport_list = resp.data.companycrimereport_list;
            });
            
            vm.submitted = false;
            vm.validateInput = function (name, type) {
                var input = vm.formValidate[name];
                return (input.$dirty || vm.submitted) && input.$error[type];
            };

            // Submit form
            vm.submitForm = function () {
                vm.submitted = true;
                if (vm.formValidate.$valid) {
                } else {
                    return false;
                }
            };         
        }

        $scope.verify_gstverification = function (gst_no, institution2branch_gid)
        {
            $scope.gst_no = gst_no;
            var params = {
                gstin: gst_no,
                institution2branch_gid: institution2branch_gid,
                application_gid: application_gid
            }
           lockUI();
            
            var url = 'api/Kyc/GSTVerification';
            SocketService.post(url, params).then(function (resp) {
              
                unlockUI();
                list();
                if (resp.data.result.gstin != "" && resp.data.result.gstin != undefined) {
               $scope.gstverification = true;
               }
                else if (resp.data.result.gstin == "" || resp.data.result.gstin == undefined) {
                    $scope.gstverification = false;
                    $scope.gstverify_status = 'notverify';
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }
        $scope.verify_gstreturnfilling = function (gst_no, institution2branch_gid) {
            $scope.gst_no = gst_no;
            var params = {
                gstin: gst_no,
                institution2branch_gid: institution2branch_gid,
                application_gid: application_gid
            }
            
            lockUI();
            var url = 'api/Kyc/GSPGSTReturnFiling';
            SocketService.post(url, params).then(function (resp) {

                unlockUI();
                list();
                if(resp.data.result != null) {
                if (resp.data.result.gstin != "" && resp.data.result.gstin != undefined) {
                    $scope.gstverification = true;
                }
                else if (resp.data.result.gstin == "" || resp.data.result.gstin == undefined) {
                    $scope.gstverification = false;
                    $scope.gstverify_status = 'notverify';
                } 
                }
                else {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
           
        }
        $scope.verify_gstauthentication = function (gst_no, institution2branch_gid) {
            $scope.gst_no = gst_no;
            var params = {
                gstin: gst_no,
                institution2branch_gid: institution2branch_gid,
                application_gid: application_gid
            }
            lockUI();

            var url = 'api/Kyc/GSTAuthentication';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                list();
                if (resp.data.statusCode == 101) {
                    $scope.panvalidation = true;
                }
            });

        }
       function list()
        {
           var params = {
               institution_gid: institution_gid
           }
           
           var url = 'api/MstAPIVerifications/GetGSTList';
           lockUI();
           SocketService.getparams(url, params).then(function (resp) {
               unlockUI();
               $scope.institutiongst_list = resp.data.mstgst_list;
               console.log(resp.data.mstgst_list)
           });
       }

       $scope.authenticationView = function (institution2branch_gid) {
           var institution2branch_gid = institution2branch_gid;
           localStorage.setItem('institution2branch_gid', institution2branch_gid);
           var URL = location.protocol + "//" + location.hostname + "/v1/#/app/GSTAuthenticationView";
           window.open(URL, '_blank');
       }

       $scope.verificationView = function (institution2branch_gid) {
           var institution2branch_gid = institution2branch_gid;
           localStorage.setItem('institution2branch_gid', institution2branch_gid);
           var URL = location.protocol + "//" + location.hostname + "/v1/#/app/GSPGSTINAuthenticationView";
           window.open(URL, '_blank');
       }

       $scope.returnfillingView = function (institution2branch_gid) {
           var institution2branch_gid = institution2branch_gid;
           localStorage.setItem('institution2branch_gid', institution2branch_gid);
           var URL = location.protocol + "//" + location.hostname + "/v1/#/app/GSPGSTReturnFilingView";
           window.open(URL, '_blank');
       }

        // TAN Verification
        $scope.onchange = function () {
            $scope.verifyvalidation = false;
            $scope.status = 'notchecked';
            $scope.verify_status = '';
        }
     
        $scope.verify_tan = function (txttan_no)
        {
            if (txttan_no == '' || txttan_no == undefined || txttan_no == null) {
                Notify.alert('Kindly Enter TAN', 'warning');
            }
            else {
                var params = {
                    tan_no: txttan_no,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/TANauthetication';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.status = 'checked';
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.verifyvalidation = true;
                    }
                    else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.verifyvalidation = false;
                        $scope.verify_status = 'notverify';
                        Notify.alert('TAN is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.addtan=function()
        {  
            if ($scope.status == 'checked') {
                var params = {
                    tan_no: $scope.txttan_no,
                    remarks: $scope.txttan_remarks,
                    function_gid: institution_gid,

                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostTAN';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();

                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetTAN';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.tan_list = resp.data.tan_list;
                        });
                        $scope.txttan_remarks = '';
                        $scope.txttan_no = '';
                        $scope.status = '';
                        $scope.verify_status = '';
                        $scope.verifyvalidation = false;
                    }
                    else
                    {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });
            }
            else {
                Notify.alert('Kindly Verify the TAN Details', 'warning')
            }
        }
        // Companyand LLP Verification
        $scope.llponchange = function () {
            $scope.llpverifyvalidation = false;
            $scope.llpstatus = 'notchecked';
            $scope.llpverify_status = '';
        }
        $scope.verify_llp = function (txtllp_no) {
            if (txtllp_no == '' || txtllp_no == undefined || txtllp_no == null) {
                Notify.alert('Kindly Enter  CIN / LLPIN / FCRN / FLLPIN', 'warning');
            }
            else {
                var params = {
                    cin_no: txtllp_no,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/CompanyLLP_no';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.llpstatus = 'checked';
                    if (resp.data.result.Company_Name != "" && resp.data.result.Company_Name != undefined) {
                        $scope.llpverifyvalidation = true;
                    }
                    else if (resp.data.result.Company_Name == "" || resp.data.result.Company_Name == undefined) {
                        $scope.llpverifyvalidation = false;
                        $scope.llpverify_status = 'notverify';
                        Notify.alert('Company & LLP Identification Number is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addllp_no = function () {
            if ($scope.llpstatus == 'checked') {
                var params = {
                    cin_no: $scope.txtllp_no,
                    remarks: $scope.txtllp_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostCompanyLLP';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();

                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetCompanyLLP';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.cin_list = resp.data.cin_list;
                        });
                        $scope.txtllp_remarks = '';
                        $scope.txtllp_no = '';
                        $scope.llpverifyvalidation = false;
                        $scope.llpstatus = '';
                        $scope.llpverify_status = '';
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
            else {
                Notify.alert('Kindly Verify the Company & LLP Identification Number', 'warning')
            }
        }

        $scope.companyLLPnoView = function (companyllpno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewCompanyandLLPNo.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       companyllpno_gid: companyllpno_gid
                   }
                var url = 'api/MstAPIVerifications/CompanyLLPViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcompany_name = resp.data.company_name;
                    $scope.txtroc_code = resp.data.roc_code;
                    $scope.txtregistration_no = resp.data.registration_no;
                    $scope.txtcompany_category = resp.data.company_category;

                    $scope.txtcompany_subcategory = resp.data.company_subcategory;
                    $scope.txtclass_of_company = resp.data.class_of_company;
                    $scope.txtnumber_of_members = resp.data.number_of_members;
                    $scope.txtdate_of_incorporation = resp.data.date_of_incorporation;

                    $scope.txtcompany_status = resp.data.company_status;
                    $scope.txtregistered_address = resp.data.registered_address;
                    $scope.txtalternative_address = resp.data.alternative_address;
                    $scope.txtemail_address = resp.data.email_address;

                    $scope.txtlisted_status = resp.data.listed_status;
                    $scope.txtsuspended_at_stock_exchange = resp.data.suspended_at_stock_exchange;
                    $scope.txtdate_of_last_AGM = resp.data.date_of_last_AGM;
                    $scope.txtdate_of_balance_sheet = resp.data.date_of_balance_sheet;

                    $scope.txtpaid_up_capital = resp.data.paid_up_capital;
                    $scope.txtauthorised_capital = resp.data.authorised_capital;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
       
        $scope.verify_fda = function (txtfda_license) {
            var params = {
                licence_no: txtfda_license,
                state: 'DL'
            }
            console.log(params)
            lockUI();

            var url = 'api/Kyc/fda';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.statusCode == 101) {
                    $scope.panvalidation = true;
                   }
            });
        }
        //MCA Signatories
        $scope.mcaonchange = function () {
            $scope.mcaverifyvalidation = false;
            $scope.mcastatus = 'notchecked';
            $scope.mcaverify_status = '';
        }
        $scope.verifymca_sign = function (txtmca_sign) {
            if (txtmca_sign == '' || txtmca_sign == undefined || txtmca_sign == null)
            {
                Notify.alert('Kindly Enter  CIN / LLPIN', 'warning');
            }
            else {
            
            var params = {
                cin: txtmca_sign,
                function_gid: institution_gid,
                application_gid: application_gid
            }
            lockUI();

            var url = 'api/Kyc/mcasignatories';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.mcastatus = 'checked';
                if (resp.data.result != "" && resp.data.result != undefined) {
                    $scope.mcaverifyvalidation = true;
                }
                else  {
                    $scope.mcaverifyvalidation = false;
                    $scope.mcaverify_status = 'notverify';
                    Notify.alert('MCA Signatories is not verified..!', 'warning');
                } 
            });
            }
        }
        $scope.addmca_sign = function () {
            if ($scope.mcastatus == 'checked')
            {
            var params = {
                cin_no: $scope.txtmca_sign,
                remarks: $scope.txtmcasign_remarks,
                function_gid: institution_gid,
            }
            lockUI();

            var url = 'api/MstAPIVerifications/PostMCASignature';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();

                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/MstAPIVerifications/GetMCASignature';
                    var params = {
                        function_gid: institution_gid,
                    }
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.mcasign_list = resp.data.cin_list;
                    });
                    $scope.txtmca_sign = '';
                    $scope.txtmcasign_remarks = '';
                    $scope.mcaverifyvalidation = false;
                    $scope.mcastatus = '';
                    $scope.mcaverify_status = '';
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
            else {
                Notify.alert('Kindly Verify the MCA Signatories', 'warning')
            }
        }

        $scope.mcasignView = function (mcasignatories_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/MCASignatoriesViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       mcasignatories_gid: mcasignatories_gid
                   }
                var url = 'api/MstAPIVerifications/MCASignatoriesViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.mcasignatorydetails_list = resp.data.mcasignatorydetails_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        //IEC Detailed
        $scope.ieconchange = function () {
            $scope.iecverifyvalidation = false;
            $scope.iecstatus = 'notchecked';
            $scope.iecverify_status = '';
        }
        $scope.verifyiec_detailed = function (txtiec_detailed) {
            if (txtiec_detailed == '' || txtiec_detailed == undefined || txtiec_detailed == null) {
                Notify.alert('Kindly Enter Import Export Code', 'warning');
            }
            else {
                var params = {
                    iec_no: txtiec_detailed,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/IECDetailed';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.iecstatus = 'checked';
                    if (resp.data.result.pan != "" && resp.data.result.pan != undefined) {
                        $scope.iecverifyvalidation = true;
                    }
                    else if (resp.data.result.pan == "" || resp.data.result.pan == undefined) {
                        $scope.iecverifyvalidation = false;
                        $scope.iecverify_status = 'notverify';
                        Notify.alert('IEC Detailed is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }   
        $scope.addiec_detailed = function () {
            if ($scope.iecstatus == 'checked')
            {
            var params = {
              remarks: $scope.txtiec_remarks,
                function_gid: institution_gid,
            }
            lockUI();

            var url = 'api/MstAPIVerifications/PostIECDetailed';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/MstAPIVerifications/GetIECDetailed';
                    var params = {
                        function_gid: institution_gid,
                    }
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.IECDetailed_list = resp.data.IECDetailed_list;
                        
                    });
                    $scope.txtiec_detailed = '';
                    $scope.txtiec_remarks = '';
                    $scope.iecverifyvalidation = false;
                    $scope.iecstatus = '';
                    $scope.iecverify_status = '';
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
            else {
                Notify.alert('Kindly Verify the IEC Details','warning')
            }
        }

        $scope.IECView = function (iecdtl_gid) {
            var iecdtl_gid = iecdtl_gid;
            localStorage.setItem('iecdtl_gid', iecdtl_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/IECDetailedProfileView";
            window.open(URL, '_blank');
        }

        //FSSAI License
        $scope.fssaionchange = function () {
            $scope.fssaiverifyvalidation = false;
            $scope.fssaistatus = 'notchecked';
            $scope.fssaiverify_status = '';
        }
        $scope.verifyfssai = function (txtreg_no) {
            if (txtreg_no == '' || txtreg_no == undefined || txtreg_no == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else {
                var params = {
                    reg_no: txtreg_no,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/FSSAI';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fssaistatus = 'checked';
                    if (resp.data.result.LicType != "" && resp.data.result.LicType != undefined) {
                        $scope.fssaiverifyvalidation = true;
                    }
                    else if (resp.data.result.LicType == "" || resp.data.result.LicType == undefined) {
                        $scope.fssaiverifyvalidation = false;
                        $scope.fssaiverify_status = 'notverify';
                        Notify.alert('FSSAI License is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addfssai = function () {
            if ($scope.fssaistatus == 'checked') {
                var params = {
                    remarks: $scope.txtfssai_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostFSSAI';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetFSSAI';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.fssai_list = resp.data.fssai_list;

                        });
                        $scope.txtreg_no= '';
                        $scope.txtfssai_remarks = '';
                        $scope.fssaiverifyvalidation = false;
                        $scope.fssaistatus = '';
                        $scope.fssaiverify_status = '';
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
            else {
                Notify.alert('Kindly Verify the FSSAI License', 'warning')
            }
        }

        $scope.FSSAIView = function (fssailicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFSSAIDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fssailicenseauthentication_gid: fssailicenseauthentication_gid
                   }
                var url = 'api/MstAPIVerifications/FSSAIViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtfssai_status = resp.data.fssai_status;
                    $scope.txtlicense_type = resp.data.license_type;
                    $scope.txtlicense_no = resp.data.license_no;
                    $scope.txtfirm_name = resp.data.firm_name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //FDA License
        $scope.fdaonchange = function () {
            $scope.fdaverifyvalidation = false;
            $scope.fdastatus = 'notchecked';
            $scope.fdaverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifyfda = function () {
            if ($scope.txtlicense_no == '' || $scope.txtlicense_no == undefined || $scope.txtlicense_no == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else {
                var params = {
                    licence_no: $scope.txtlicense_no,
                    state:$scope.cbostate.state_code,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/fda';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fdastatus = 'checked';
                    if (resp.data.result.store_name != "" && resp.data.result.store_name != undefined) {
                        $scope.fdaverifyvalidation = true;
                    }
                    else if (resp.data.result.store_name == "" || resp.data.result.store_name == undefined) {
                        $scope.fdaverifyvalidation = false;
                        $scope.fdaverify_status = 'notverify';
                        Notify.alert('FDA License Number is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addfda = function () {
            if ($scope.fdastatus == 'checked') {
                var params = {
                    remarks: $scope.txtfda_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostFDA';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetFDA';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.fda_list = resp.data.fda_list;

                        });
                        $scope.txtlicense_no = '';
                        $scope.txtfda_remarks = '';
                        $scope.fdaverifyvalidation = false;
                        $scope.fdastatus = '';
                        $scope.fdaverify_status = '';
                        $scope.cbostate = '';
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
            else {
                Notify.alert('Kindly Verify the FDA License', 'warning')
            }
        }

        $scope.FDAView = function (fdalicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFDADetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fdalicenseauthentication_gid: fdalicenseauthentication_gid
                   }
                var url = 'api/MstAPIVerifications/FDAViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstore_name = resp.data.store_name;
                    $scope.txtcontact_no = resp.data.contact_no;
                    $scope.txtlicense_detail = resp.data.license_detail;
                    $scope.txtname = resp.data.name;
                    $scope.txtaddress = resp.data.address;
                    
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //LPG ID
        $scope.lpgidonchange = function () {
            $scope.lpgidverifyvalidation = false;
            $scope.lpgidstatus = 'notchecked';
            $scope.lpgidverify_status = '';
        }
        $scope.verifylpgid = function (txtlpgid) {
            if (txtlpgid == '' || txtlpgid == undefined || txtlpgid == null) {
                Notify.alert('Kindly Enter Import Export Code', 'warning');
            }
            else {
                var params = {
                    lpg_id: txtlpgid,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/LPGIDAuthentication';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lpgidstatus = 'checked';
                    if (resp.data.result.ConsumerName != "" && resp.data.result.ConsumerName != undefined) {
                        $scope.lpgidverifyvalidation = true;
                    }
                    else if (resp.data.result.ConsumerName == "" || resp.data.result.ConsumerName == undefined) {
                        $scope.lpgidverifyvalidation = false;
                        $scope.lpgidverify_status = 'notverify';
                        Notify.alert('LPG ID Detailed is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addlpgid = function () {
            if ($scope.lpgidstatus == 'checked') {
                var params = {
                    remarks: $scope.txtlpgid_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostLPGID';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetLPGIDList';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.LPGID_list = resp.data.LPGID_list;

                        });
                        $scope.txtlpgid = '';
                        $scope.txtlpgid_remarks = '';
                        $scope.lpgidverifyvalidation = false;
                        $scope.lpgidstatus = '';
                        $scope.lpgidverify_status = '';
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
            else {
                Notify.alert('Kindly Verify the LPG ID Details', 'warning')
            }
        }

        $scope.LPGIDView = function (lpgiddtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewLPGID.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       lpgiddtl_gid: lpgiddtl_gid
                   }
                var url = 'api/MstAPIVerifications/LPGIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtApproximateSubsidyAvailed = resp.data.result.ApproximateSubsidyAvailed;
                    $scope.txtSubsidizedRefillConsumed = resp.data.result.SubsidizedRefillConsumed;
                    $scope.txtpin = resp.data.result.pin;

                    $scope.txtConsumerEmail = resp.data.result.ConsumerEmail;
                    $scope.txtDistributorCode = resp.data.result.DistributorCode;
                    $scope.txtBankName = resp.data.result.BankName;
                    $scope.txtIFSCCode = resp.data.result.IFSCCode;

                    $scope.txtAadhaarNo = resp.data.result.AadhaarNo;
                    $scope.txtConsumerContact = resp.data.result.ConsumerContact;
                    $scope.txtDistributorAddress = resp.data.result.DistributorAddress;
                    $scope.txtConsumerName = resp.data.result.ConsumerName;

                    $scope.txtConsumerNo = resp.data.result.ConsumerNo;
                    $scope.txtDistributorName = resp.data.result.DistributorName;
                    $scope.txtBankAccountNo = resp.data.result.BankAccountNo;
                    $scope.txtGivenUpSubsidy = resp.data.result.GivenUpSubsidy;

                    $scope.txtConsumerAddress = resp.data.result.ConsumerAddress;
                    $scope.txtLastBookingDate = resp.data.result.LastBookingDate;
                    $scope.txtTotalRefillConsumed = resp.data.result.TotalRefillConsumed;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //SHOP AND ESTABLISHMENT

        $scope.shoponchange = function () {
            $scope.shopverifyvalidation = false;
            $scope.shopstatus = 'notchecked';
            $scope.shopverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifyshop = function () {
            if ($scope.txtregNo == '' || $scope.txtregNo == undefined || $scope.txtregNo == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else {
                var params = {
                    regNo: $scope.txtregNo,
                    areaCode: $scope.cbostate.state_code,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/ShopAndEstablishment';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.shopstatus = 'checked';
                    if (resp.data.statusCode == 101) {
                        $scope.shopverifyvalidation = true;
                    }
                    else if (resp.data.statusCode == 103) {
                        $scope.shopverifyvalidation = false;
                        $scope.shopverify_status = 'notverify';
                        Notify.alert('Shop Registration Number is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addshop = function () {
            if ($scope.shopstatus == 'checked') {
                var params = {
                    remarks: $scope.txtshop_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostShop';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetShopList';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.shop_list = resp.data.shop_list;

                        });
                        $scope.txtregNo = '';
                        $scope.txtshop_remarks = '';
                        $scope.shopverifyvalidation = false;
                        $scope.shopstatus = '';
                        $scope.shopverify_status = '';
                        $scope.cbostate = '';
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
            else {
                Notify.alert('Kindly Verify the Shop', 'warning')
            }
        }

        $scope.ShopView = function (shopandestablishment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewShopDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       shopandestablishment_gid: shopandestablishment_gid
                   }
                var url = 'api/MstAPIVerifications/ShopViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcategory = resp.data.result.category;
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtcommenceDate = resp.data.result.commenceDate;
                    $scope.txttotalWorkers = resp.data.result.totalWorkers;

                    $scope.txtfatherNameOfOccupier = resp.data.result.fatherNameOfOccupier;
                    $scope.txtemail = resp.data.result.email;
                    $scope.txtwebsiteUrl = resp.data.result.websiteUrl;
                    $scope.txtpdfLink = resp.data.result.pdfLink;

                    $scope.txtownerName = resp.data.result.ownerName;
                    $scope.txtaddress = resp.data.result.address;
                    $scope.txtapplicantName = resp.data.result.applicantName;
                    $scope.txtvalidFrom = resp.data.result.validFrom;

                    $scope.txtnatureOfBusiness = resp.data.result.natureOfBusiness;
                    $scope.txtvalidTo = resp.data.result.validTo;
                    $scope.txtregistrationDate = resp.data.result.registrationDate;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //RC Auth Advanced
        $scope.rcauthadvonchange = function () {
            $scope.rcauthadvverifyvalidation = false;
            $scope.rcauthadvstatus = 'notchecked';
            $scope.rcauthadvverify_status = '';
        }
        $scope.verifyrcauthadv = function (txtregistrationNumber) {
            if (txtregistrationNumber == '' || txtregistrationNumber == undefined || txtregistrationNumber == null) {
                Notify.alert('Kindly Enter Import Export Code', 'warning');
            }
            else {
                var params = {
                    registrationNumber: txtregistrationNumber,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/VehicleRCAuthAdvanced';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rcauthadvstatus = 'checked';
                    if (resp.data.statusCode == 101) {
                        $scope.rcauthadvverifyvalidation = true;
                    }
                    else if (resp.data.statusCode == 103) {
                        $scope.rcauthadvverifyvalidation = false;
                        $scope.rcauthadvverify_status = 'notverify';
                        Notify.alert('RC Detail is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addrcauthadv = function () {
            if ($scope.rcauthadvstatus == 'checked') {
                var params = {
                    remarks: $scope.txtrcauthadv_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostRCAuthAdvanced';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetRCAuthAdvancedList';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;

                        });
                        $scope.txtregistrationNumber = '';
                        $scope.txtrcauthadv_remarks = '';
                        $scope.rcauthadvverifyvalidation = false;
                        $scope.rcauthadvstatus = '';
                        $scope.rcauthadvverify_status = '';
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
            else {
                Notify.alert('Kindly Verify the RC Details', 'warning')
            }
        }
        $scope.RCAuthAdvancedView = function (vehiclercauthadvanced_gid) {
            var vehiclercauthadvanced_gid = vehiclercauthadvanced_gid;
            localStorage.setItem('vehiclercauthadvanced_gid', vehiclercauthadvanced_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/RCAuthAdvancedView";
            window.open(URL, '_blank');
        }

        //RC Search

        $scope.rcsearchonchange = function () {
            $scope.rcsearchverifyvalidation = false;
            $scope.rcsearchstatus = 'notchecked';
            $scope.rcsearchverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifyrcsearch = function () {
            if ($scope.txtengine_no == '' || $scope.txtengine_no == undefined || $scope.txtengine_no == null) {
                Notify.alert('Kindly Enter Engine Number', 'warning');
            }
            else {
                var params = {
                    engine_no: $scope.txtengine_no,
                    chassis_no: $scope.txtchassis_no,
                    state: $scope.cbostate.state_code,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/VehicleRCSearch';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rcsearchstatus = 'checked';
                    if(resp.data.result != null) {
                    if (resp.data.result.rc_regn_no != "" && resp.data.result.rc_regn_no != undefined) {
                        $scope.rcsearchverifyvalidation = true;
                    }
                    else if (resp.data.result.rc_regn_no == "" || resp.data.result.rc_regn_no == undefined) {
                        $scope.rcsearchverifyvalidation = false;
                        $scope.rcsearchverify_status = 'notverify';
                        Notify.alert('Engine Number is not verified..!', 'warning');
                    } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addrcsearch = function () {
            if ($scope.rcsearchstatus == 'checked') {
                var params = {
                    remarks: $scope.txtrcsearch_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostRCSearch';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetRCSearchList';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.RCSearch_list = resp.data.RCSearch_list;

                        });
                        $scope.txtengine_no = '';
                        $scope.txtchassis_no = '';
                        $scope.txtrcsearch_remarks = '';
                        $scope.rcsearchverifyvalidation = false;
                        $scope.rcsearchstatus = '';
                        $scope.rcsearchverify_status = '';
                        $scope.cbostate = '';
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
            else {
                Notify.alert('Kindly Verify the Engine Number', 'warning')
            }
        }

        $scope.RCSearchView = function (vehiclercsearch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewRCSearchDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       vehiclercsearch_gid: vehiclercsearch_gid
                   }
                var url = 'api/MstAPIVerifications/RCSearchViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrc_manu_month_yr = resp.data.result.rc_manu_month_yr;
                    $scope.txtrc_maker_model = resp.data.result.rc_maker_model;
                    $scope.txtrc_f_name = resp.data.result.rc_f_name;
                    $scope.txtrc_eng_no = resp.data.result.rc_eng_no;

                    $scope.txtrc_owner_name = resp.data.result.rc_owner_name;
                    $scope.txtrc_vh_class_desc = resp.data.result.rc_vh_class_desc;
                    $scope.txtrc_present_address = resp.data.result.rc_present_address;
                    $scope.txtrc_color = resp.data.result.rc_color;

                    $scope.txtrc_regn_no = resp.data.result.rc_regn_no;
                    $scope.txttax_paid_upto = resp.data.result.tax_paid_upto;
                    $scope.txtrc_maker_desc = resp.data.result.rc_maker_desc;
                    $scope.txtrc_chasi_no = resp.data.result.rc_chasi_no;

                    $scope.txtrc_mobile_no = resp.data.result.rc_mobile_no;
                    $scope.txtrc_registered_at = resp.data.result.rc_registered_at;
                    $scope.txtrc_valid_upto = resp.data.result.rc_valid_upto;
                    $scope.txtrc_regn_dt = resp.data.result.rc_regn_dt;

                    $scope.txtrc_financer = resp.data.result.rc_financer;
                    $scope.txtrc_permanent_address = resp.data.result.rc_permanent_address;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        //Property Tax

        $scope.propertytaxonchange = function () {
            $scope.propertytaxverifyvalidation = false;
            $scope.propertytaxstatus = 'notchecked';
            $scope.propertytaxverify_status = '';
            $scope.cbostate = '';
        }
        $scope.verifypropertytax = function () {
            if ($scope.txtpropertyNo == '' || $scope.txtpropertyNo == undefined || $scope.txtpropertyNo == null) {
                Notify.alert('Kindly Enter Registration Number', 'warning');
            }
            else {
                var params = {
                    propertyNo: $scope.txtpropertyNo,
                    city: $scope.txtcity,
                    state: $scope.cbostate.gst_state,
                    district: $scope.txtdistrict,
                    ulb: $scope.txtulb,
                    function_gid: institution_gid,
                    application_gid: application_gid
                }
                lockUI();

                var url = 'api/Kyc/PropertyTax';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.propertytaxstatus = 'checked';
                    if (resp.data.statusCode == 101) {
                        $scope.propertytaxverifyvalidation = true;
                    }
                    else if (resp.data.statusCode == 103) {
                        $scope.propertytaxverifyvalidation = false;
                        $scope.propertytaxverify_status = 'notverify';
                        Notify.alert('Property Tax is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        $scope.addpropertytax = function () {
            if ($scope.propertytaxstatus == 'checked') {
                var params = {
                    remarks: $scope.txtpropertytax_remarks,
                    function_gid: institution_gid,
                }
                lockUI();

                var url = 'api/MstAPIVerifications/PostPropertyTax';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var url = 'api/MstAPIVerifications/GetPropertyTaxList';
                        var params = {
                            function_gid: institution_gid,
                        }
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.PropertyTax_list = resp.data.PropertyTax_list;

                        });
                        $scope.txtpropertyNo = '';
                        $scope.txtcity = '';
                        $scope.txtdistrict = '';
                        $scope.txtulb = '';
                        $scope.txtpropertytax_remarks = '';
                        $scope.propertytaxverifyvalidation = false;
                        $scope.propertytaxstatus = '';
                        $scope.propertytaxverify_status = '';
                        $scope.cbostate = '';
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
            else {
                Notify.alert('Kindly Verify the Property Tax', 'warning')
            }
        }

        $scope.PropertyTaxView = function (propertytax_gid) {
            var propertytax_gid = propertytax_gid;
            localStorage.setItem('propertytax_gid', propertytax_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/PropertyTaxView";
            window.open(URL, '_blank');
        }

        //Probe 42

       
        $scope.baseDetails = function () {
            var probecompanypan_no = $scope.probecompanypan_no;

            

                var params = {
                    application_gid: application_gid,
                    institution_gid: institution_gid,
                    pan: probecompanypan_no,
                }

                lockUI();
                var url = 'api/ProbeAPI/GetBaseDetails';
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
                    institutionprobe_list();
                   
                });
           

                          
        }

        $scope.comprehensiveDetails = function () {

            var probecompanypan_no = $scope.probecompanypan_no;

            
                var params = {
                    application_gid: application_gid,
                    institution_gid: institution_gid,
                    pan: probecompanypan_no,
                }

                lockUI();
                var url = 'api/ProbeAPI/GetComprehensiveDetails';
                SocketService.post(url, params).then(function (resp) {
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
                    institutionprobe_list();
                    unlockUI();
                });
            
          
        }

        $scope.dataStatus = function () {
            var probecompanypan_no = $scope.probecompanypan_no;
            var params = {
                application_gid: application_gid,
                institution_gid: institution_gid,
                pan: probecompanypan_no,
            }

            lockUI();
            var url = 'api/ProbeAPI/GetDataStatus';
            SocketService.post(url, params).then(function (resp) {
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
                institutionprobe_list();
                unlockUI();
            });
        }

        function institutionprobe_list() {
            var params = {             
                institution_gid: institution_gid,            
            }
            var url = 'api/ProbeAPI/InstitutionProbeList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionprobe_list = resp.data.institutionprobe_list;
            });
        }
    
        
            

        $scope.ProbeDetailsView = function (data) {
            if (data.api_name == "Base Details") {
                var institutionprobedetails_gid = data.institutionprobedetails_gid;                
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/BaseDetailsView?institutionprobedetails_gid=" + institutionprobedetails_gid;
                window.open(URL, '_blank');
            }
            else if (data.api_name == "Comprehensive Details") {
                var institutionprobedetails_gid = data.institutionprobedetails_gid;              
                var URL = location.protocol + "//" + location.hostname + "/v1/#/app/ComprehensiveDetailsView?institutionprobedetails_gid=" + institutionprobedetails_gid + "&lsdetail_name=BASICDETAIL";
                window.open(URL, '_blank');
            }
            else if (data.api_name == "Data Status") {
                var institutionprobedetails_gid = data.institutionprobedetails_gid;
                ProbeDataStatusView(institutionprobedetails_gid);
            }
            
        }

        function ProbeDataStatusView(institutionprobedetails_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewProbeDataStatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       institutionprobedetails_gid: institutionprobedetails_gid
                   }
                var url = 'api/ProbeAPI/DataStatusView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.status_details = resp.data.data.data_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.articleOfAssociationDoc = function () {
            var probecompanypan_no = $scope.probecompanypan_no;
            var params = {
                application_gid: application_gid,
                institution_gid: institution_gid,
                pan: probecompanypan_no,
            }

            lockUI();
            var url = 'api/ProbeAPI/GetArticleOfAssociationDoc';
            SocketService.post(url, params).then(function (resp) {
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
                institutionprobedoc_list();
                unlockUI();
            });
        }

        $scope.memorandumOfAssociationDoc = function () {
            var probecompanypan_no = $scope.probecompanypan_no;
            var params = {
                application_gid: application_gid,
                institution_gid: institution_gid,
                pan: probecompanypan_no,
            }

            lockUI();
            var url = 'api/ProbeAPI/GetMemorandumOfAssociationDoc';
            SocketService.post(url, params).then(function (resp) {
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
                institutionprobedoc_list();
                unlockUI();
            });
        }

        $scope.certificateOfIncorporationDoc = function () {
            var probecompanypan_no = $scope.probecompanypan_no;
            var params = {
                application_gid: application_gid,
                institution_gid: institution_gid,
                pan: probecompanypan_no,
            }

            lockUI();
            var url = 'api/ProbeAPI/GetCertificateOfIncorporationDoc';
            SocketService.post(url, params).then(function (resp) {
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
                institutionprobedoc_list();
                unlockUI();
            });
        }

        $scope.mgt7formDoc = function () {
            var probecompanypan_no = $scope.probecompanypan_no;
            var params = {
                application_gid: application_gid,
                institution_gid: institution_gid,
                pan: probecompanypan_no,
            }

            lockUI();
            var url = 'api/ProbeAPI/GetFormMGT7Doc';
            SocketService.post(url, params).then(function (resp) {
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
                institutionprobedoc_list();
                unlockUI();
            });
        }

        $scope.aoc4formDoc = function () {
            var probecompanypan_no = $scope.probecompanypan_no;
            var params = {
                application_gid: application_gid,
                institution_gid: institution_gid,
                pan: probecompanypan_no,
            }

            lockUI();
            var url = 'api/ProbeAPI/GetFormAOC4Doc';
            SocketService.post(url, params).then(function (resp) {
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
                institutionprobedoc_list();
                unlockUI();
            });
        }

        function institutionprobedoc_list() {
            var params = {
                institution_gid: institution_gid,
            }
            var url = 'api/ProbeAPI/InstitutionProbeDocList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionprobedoc_list = resp.data.institutionprobedoc_list;
            });
        }
        

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.Back = function () {
            if (lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/MstDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/MstCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_economicadd = function () {
            $location.url('app/MstCreditEconomicCapitalAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/MstCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/MstCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/MstCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/MstCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/MstCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/MstCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/MstCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/MstCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.institution_bureauadd = function () {
            $location.url('app/MstCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {

            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
            activate(); window.scroll(0, 0)
        }
        $scope.companyllpno_vertification = function () {

            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
            activate(); window.scroll(0, 0)
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
            activate(); window.scroll(0, 0)
        }
        $scope.iecdetailed_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
            activate();            
        }
        $scope.fssai_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
            activate();
        }
        $scope.fda_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
            activate();
        }
        $scope.gst_verification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
            activate();
        }
        $scope.lpgid_verification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
            activate();
        }
        $scope.shop_verification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
            activate();
        }
        $scope.rcauthadv_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
            activate();
        }
        $scope.rcsearch_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
            activate();
        }
        $scope.propertytax_vertification = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
            activate();
        }
        $scope.probe42_api = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROBEAPI' + '&lspage=' + lspage);
            activate();
        }
        $scope.crimecheck_record = function () {
            $location.url('app/MstCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            window.scroll(0, 0);
            $location.url('app/MstCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
            activate();
        }
        $scope.company_bankstatement = function () {
            $location.url('app/MstCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/MstCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_addguarantee = function () {
            $location.url('app/MstCreditGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_addcolending = function () {
            $location.url('app/MstCreditColendingDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        //Delete Events
      
        $scope.tandelete = function (tandtl_gid) {
            var params =
                {
                    tandtl_gid: tandtl_gid
                }
            var url = 'api/MstAPIVerifications/TANdelete';
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
                var url = 'api/MstAPIVerifications/GetTAN';
                var params = {
                    function_gid: institution_gid,
                }
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.tan_list = resp.data.tan_list;
                });
            });

        }
        $scope.companyLLPnoDelete = function (companyllpno_gid) {
            var params =
                {
                    companyllpno_gid: companyllpno_gid
                }
            var url = 'api/MstAPIVerifications/CompanyLLPNodelete';
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
                var url = 'api/MstAPIVerifications/GetCompanyLLP';
                var params = {
                    function_gid: institution_gid,
                }
                lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.cin_list = resp.data.cin_list;
                });
            });

        }
        $scope.mcasignDelete = function (mcasignatories_gid) {
            var params =
                {
                    mcasignatories_gid: mcasignatories_gid
                }
            var url = 'api/MstAPIVerifications/MCASigndelete';
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
                var url = 'api/MstAPIVerifications/GetMCASignature';
                var params = {
                    function_gid: institution_gid,
                }
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.mcasign_list = resp.data.cin_list;
                });
            });

        }
        $scope.IECdelete = function (iecdtl_gid) {
            var params =
                {
                    iecdtl_gid: iecdtl_gid
                }
            var url = 'api/MstAPIVerifications/IECdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetIECDetailed';
               lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.IECDetailed_list = resp.data.IECDetailed_list;
                });
            });

        }
        $scope.FSSAIdelete = function (fssailicenseauthentication_gid) {
            var params =
                {
                    fssailicenseauthentication_gid: fssailicenseauthentication_gid
                }
            var url = 'api/MstAPIVerifications/FSSAIdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetFSSAI';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fssai_list = resp.data.fssai_list;
                });
            });

        }
        $scope.FDAdelete = function (fdalicenseauthentication_gid) {
            var params =
                {
                    fdalicenseauthentication_gid: fdalicenseauthentication_gid
                }
            var url = 'api/MstAPIVerifications/FDAdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetFDA';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.fda_list = resp.data.fda_list;
                });
            });

        }

        $scope.LPGIDdelete = function (lpgiddtl_gid) {
            var params =
                {
                    lpgiddtl_gid: lpgiddtl_gid
                }
            var url = 'api/MstAPIVerifications/LPGIDdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetLPGIDList';
               lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.LPGID_list = resp.data.LPGID_list;
                });
            });

        }

        $scope.Shopdelete = function (shopandestablishment_gid) {
            var params =
                {
                    shopandestablishment_gid: shopandestablishment_gid
                }
            var url = 'api/MstAPIVerifications/Shopdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetShopList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.shop_list = resp.data.shop_list;
                });
            });

        }

        $scope.RCAuthAdvanceddelete = function (vehiclercauthadvanced_gid) {
            var params =
                {
                    vehiclercauthadvanced_gid: vehiclercauthadvanced_gid
                }
            var url = 'api/MstAPIVerifications/RCAuthAdvanceddelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetRCAuthAdvancedList';
               lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
                });
            });

        }

        $scope.RCSearchdelete = function (vehiclercsearch_gid) {
            var params =
                {
                    vehiclercsearch_gid: vehiclercsearch_gid
                }
            var url = 'api/MstAPIVerifications/RCSearchdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetRCSearchList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.RCSearch_list = resp.data.RCSearch_list;
                });
            });

        }

        $scope.PropertyTaxdelete = function (propertytax_gid) {
            var params =
                {
                    propertytax_gid: propertytax_gid
                }
            var url = 'api/MstAPIVerifications/PropertyTaxdelete';
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
                var params = {
                    function_gid: institution_gid,
                }
                var url = 'api/MstAPIVerifications/GetPropertyTaxList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.PropertyTax_list = resp.data.PropertyTax_list;
                });
            });

        }

    //CrimeCheck API

    $scope.raise_crimereportrequest = function () {
       
        if($scope.cboaddress == '' || $scope.cboaddress == null || $scope.cboaddress == undefined ) {
            Notify.alert("Kindly select the address..!",'warning');
        } else {
            var params = {
                institution_gid: $scope.institution_gid,
                company_name: $scope.company_name,
                company_address: $scope.cboaddress.address,
                application_gid: $scope.application_gid,
                report_mode: 'RealTime'
            }
            lockUI();
    
            var url = 'api/CrimeCheckAPI/RequestCrimeReportCompany';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
               
                if (resp.data.status == "OK") {
                    Notify.alert(resp.data.requestStatusMessage, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    companycrimereport_list();
                }
                else {
                    Notify.alert(resp.data.requestStatusMessage, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            
        }
            
        

    }
    

    function companycrimereport_list() {
        var paramcrime = {
            institution_gid: $scope.institution_gid
        }
        var url = 'api/CrimeCheckAPI/GetCompanyReportList';
        lockUI();
        SocketService.getparams(url, paramcrime).then(function (resp) {
            unlockUI();
            $scope.companycrimereport_list = resp.data.companycrimereport_list;
        });
    }

    $scope.crimereport_view = function (crimereportinstitution_gid) {
        var crimereportinstitution_gid = crimereportinstitution_gid;
        localStorage.setItem('crimereportinstitution_gid', crimereportinstitution_gid);
        var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCrimeReportCompanyView";
        window.open(URL, '_blank');
    }

    $scope.download_crimereport = function (val1, val2) {          
        var link = document.createElement("a");
        link.download = val2;
        var uri = val1;
        link.href = uri;
        link.click();
    }
    


    }
})();
