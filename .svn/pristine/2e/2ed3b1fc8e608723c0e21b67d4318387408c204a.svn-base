(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnCourierCreation', idasTrnCourierCreation);

    idasTrnCourierCreation.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$filter', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnCourierCreation($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $filter, DownloaddocumentService,cmnfunctionService) {
        $scope.title = 'idasTrnCourierCreation';
        var vm = this;
        vm.title = 'idasTrnCourierCreation';
        
        activate();

        function activate() {

            $scope.rdbcustomer = 'Yes';
                $scope.customerno = false;
                $scope.customeryes = true;

            $scope.courier_type=localStorage.getItem('courier_type');
            
          
            if($scope.courier_type=="Physical Inward" || $scope.courier_type=="Physical Outward"){
                document.getElementById("txtcourier_company_name").disabled = false;
                $scope.txtcourier_company_name="";
                $scope.txtpod_no="";
                $scope.isDisabled=true;
               
             }
             else{
                $scope.isDisabled=false;
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

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/IdasMstCourierCompany/CourierCompanySummary';
            SocketService.get(url).then(function (resp) {
                $scope.couriercompany_list = resp.data.MdlCourierCompany;
            });


            var params = {
                courierdocument_gid: "undefine"
            }
            var url = 'api/IdasCourierManagement/DeleteCourierDoc';
           
            SocketService.getparams(url, params).then(function (resp) {
               

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
            if($scope.courier_type=='Courier Inward'){
                $location.url('app/idasCourierMgmtsummary?lstab=CI');
               }
               else if($scope.courier_type=='Courier Outward'){
                $location.url('app/idasCourierMgmtsummary?lstab=CO');
               }
               else if($scope.courier_type=='Physical Outward'){
                $location.url('app/idasCourierMgmtsummary?lstab=PO');
               }
               else if($scope.courier_type=='Physical Inward')
               {
                $location.url('app/idasCourierMgmtsummary?lstab=PI');
               }
               else{
                $location.url('app/idasCourierMgmtsummary?lstab=CI');
               }
               
        }

        $scope.OnChangeCourierType=function(val){
            
            if(val=="Physical Inward" || val=="Physical Outward"){
               $scope.txtcourier_company_name="";
               $scope.txtpod_no="";
               $scope.isDisabled=true;
            }
            else{
                $scope.isDisabled=false;
            }
        }
        $scope.onselectedchangecustomer = function (customer) {
            $scope.customer_gid = localStorage.setItem('onchangecustomer_gid', customer);
            var params = {
                customer_gid: $scope.cbocustomergid.customer_gid

            }
          
            var url = 'api/IdasTrnLsaManagement/customer2sanction';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer2sanction_list = resp.data.customer2sanction_list;

            });

        }
        $scope.onselectedchangesanction = function (sanction) {

            var params = {
                customer2sanction_gid: $scope.cbocustomer2sanction_gid.customer2sanction_gid

            }
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
                    var url = 'api/IdasCourierManagement/DeleteCourierDoc';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            SweetAlert.swal('Document Deleted Successfully!');
        
                            var url = 'api/IdasCourierManagement/GetCourierDoc';
        
                            SocketService.get(url).then(function (resp) {
        
                                $scope.commondocument = resp.data.uploadcourierdocument;
        
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

            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.commondocumentupload = function (val, val1, name) {
            for (var i in $scope.documentname) {
                console.log('1');
            }
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

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
            frm.append('document_gid', $scope.document_gid);
            frm.append('Trn_Gid',"");
            frm.append('document_title',$scope.txtdocument_title);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasCourierManagement/CourierDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {


                $("#commonupload").val('');
               
                $scope.txtdocument_title='';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IdasCourierManagement/GetCourierDoc';

                    SocketService.get(url).then(function (resp) {

                        $scope.commondocument = resp.data.uploadcourierdocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }

        $scope.courierSubmit = function () {
            var courier_company_name;
            if($scope.courier_type=="Courier Inward" || $scope.courier_type=="Courier Outward"){
                if($scope.txtcourier_company_name==undefined ||$scope.txtcourier_company_name=="" ){
                    Notify.alert('Select the Courier Company');
                    return;
                }
                else{
                    courier_company_name=$scope.txtcourier_company_name.couriercompany_name;
                }

                if($scope.txtpod_no==undefined||$scope.txtpod_no==""){
                    Notify.alert('Enter the POD No.');
                    return;
                }
            }
            else{
                courier_company_name= "";
                $scope.txtpod_no="";
            }
            if($scope.cbocustomer2sanction_gid==undefined){
                $scope.sanctionref_no='';
                $scope.sanction_gid='';
            }
            else{
                $scope.sanctionref_no= $scope.cbocustomer2sanction_gid.sanctionref_no;
                $scope.sanction_gid= $scope.cbocustomer2sanction_gid.customer2sanction_gid;
            }
            if ($scope.rdbcustomer == "No")
            {
                $scope.customer_name = $scope.txtcustomer_name;
                $scope.customer_gid = '';
            }
            else {
                $scope.customer_name = $scope.cbocustomergid.customername;
                $scope.customer_gid = $scope.cbocustomergid.customer_gid;
            }
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
                couriercompany_name: courier_company_name,
                address: $scope.txtaddress1,
                courier_type: $scope.courier_type,
                remarks: $scope.txtremarks,  
            }
            console.log(params);
            
           
            var url = 'api/IdasCourierManagement/IdasCourierSubmit';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    console.log('courier type',$scope.courier_type);
                   if($scope.courier_type=='Courier Inward'){
                    $location.url('app/idasCourierMgmtsummary?lstab=CI');
                   }
                   else if($scope.courier_type=='Courier Outward'){
                    $location.url('app/idasCourierMgmtsummary?lstab=CO');
                   }
                   else if($scope.courier_type=='Physical Outward'){
                    $location.url('app/idasCourierMgmtsummary?lstab=PO');
                   }
                   else if($scope.courier_type=='Physical Inward')
                   {
                    $location.url('app/idasCourierMgmtsummary?lstab=PI');
                   }
                   
                   // $state.go('app.idasCourierMgmtsummary');
                    Notify.alert(resp.data.message,'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message,'warning')
                }
                activate();
            });
        }
    }
})();