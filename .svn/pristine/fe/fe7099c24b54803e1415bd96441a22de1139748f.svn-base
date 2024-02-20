(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnCourierView', idasTrnCourierView);

    idasTrnCourierView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function idasTrnCourierView($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
       
        $scope.title = 'idasTrnCourierView';
        var vm = this;
        vm.title = 'idasTrnCourierView';
        var courier_gid;
        var page;
        activate();

        function activate() {
            $scope.courier_value=true;
            $scope.physical_value=false;

            $scope.courier_inward=true;
            $scope.courier_outward=false;
            $scope.physical_inward=false;
            $scope.physical_outward=false;

            courier_gid=localStorage.getItem('courier_gid');   
            page=localStorage.getItem('page');
            $scope.courier_gid=courier_gid;
           
            var  params={
                courier_gid:courier_gid
            }
            var url = 'api/IdasCourierManagement/GetEditCourierDetail';
            SocketService.getparams(url,params).then(function (resp) {
                console.log(resp.data);
                if(resp.data.status==true){
                    $scope.customer_name=resp.data.customer_name;
                    $scope.courierref_no=resp.data.courierref_no;
                    $scope.date_of_courier=resp.data.date_of_courier;
                   //$scope.cbocustomer2sanction_gid=resp.data.sanction_gid;
                    $scope.document_type=resp.data.document_type;
                    $scope.address=resp.data.address;
                    $scope.sender_name=resp.data.sender_name;
                    $scope.pod_no=resp.data.pod_no;
                    $scope.courier_company_name=resp.data.couriercompany_name;
                    $scope.courierhandover_to=resp.data.courierhandover_to;
                   // $scope.courierhandover_to_gid=resp.data.courierhandover_to;
                    $scope.courier_type=resp.data.courier_type;
                    $scope.remarks=resp.data.remarks;
                  //  $scope.courier_sender_name=resp.data.sender_gid;
                    $scope.handover_name=resp.data.courierhandover_to_gid;
                    $scope.uploadDoc_list=resp.data.uploadcourierdocument;
                    $scope.sanctionref_no=resp.data.sanctionref_no;
                    $scope.ack_status=resp.data.ack_status;
                    $scope.ack_date=resp.data.ack_date;
                    $scope.ackby_name=resp.data.ackby_name;
                    $scope.created_by = resp.data.created_by;
                    $scope.created_date = resp.data.created_date;
                }
                
                if($scope.courier_type=="Courier Outward" || $scope.courier_type=="Courier Inward"){
                    $scope.courier_value=true;
                    $scope.physical_value=false;
                }
                if($scope.courier_type=="Courier Outward"){
                    $scope.courier_outward=true;
                    $scope.courier_inward=false;
                    $scope.physical_inward=false;
                    $scope.physical_outward=false;
                
                
                }
    
                if($scope.courier_type=="Courier Inward"){
                    //$scope.courier_inward=true;
    
                    $scope.courier_outward=false;
                    $scope.courier_inward=true;
                    $scope.physical_inward=false;
                    $scope.physical_outward=false;
                
                }
    
                if($scope.courier_type=="Physical Inward"){
                  //  $scope.physical_inward=true;
    
                    $scope.courier_outward=false;
                    $scope.courier_inward=false;
                    $scope.physical_inward=true;
                    $scope.physical_outward=false;
                
                }
                if($scope.courier_type=="Physical Outward"){
                  //  $scope.physical_outward=true;
    
                    $scope.courier_outward=false;
                    $scope.courier_inward=false;
                    $scope.physical_inward=false;
                    $scope.physical_outward=true;
                
                }
                if($scope.courier_type=="Physical Inward" || $scope.courier_type=="Physical Outward"){
                    $scope.courier_value=false;
                    $scope.physical_value=true;
                }
    
            });

          
            console.log('courier', $scope.courier_value);
           

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

       
         $scope.courierback = function () {
            
            $location.url('app/idasCourierMgmtsummary?lstab='+page);
        }

       
    }
})();
