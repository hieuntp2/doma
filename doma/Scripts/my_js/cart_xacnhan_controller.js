﻿app.controller('cart_xacnhan_controller', ['$scope', '$rootScope', '$http', '$window', 'my_function_services', 'cart_service',
    function ($scope, $rootScope, $http, $window, my_function_services, cart_service) {
        $scope.products = [];
        $scope.totalamount = 0;
        $scope.diachi = "";
        $scope.dienthoai = "";
        $scope.thoigian = "";
        $scope.chitiets = [];
        $scope.accecttoken = "";
        $scope.init = function (diachi, dienthoai, thoigian) {
            cart_service.remove_double_item();
            $scope.products = cart_service.getcart();
            $scope.counttotalamount();

            $scope.diachi = diachi,
            $scope.dienthoai = dienthoai,
            $scope.thoigian = thoigian;

            var form = $('#__AjaxAntiForgeryForm');
            $scope.accecttoken = $('input[name="__RequestVerificationToken"]', form).val();
            $scope.converttochiviets();
        }

        $scope.counttotalamount = function () {
            $scope.products = cart_service.getcart();
            $scope.totalamount = 0;
            for (var i = 0; i < $scope.products.length; i++) {
                $scope.totalamount += $scope.products[i].price * $scope.products[i].number;
            }
        }

        $scope.chooseproduct = function (id) {
            $rootScope.$broadcast('them_gio_hang_modal_controller::show', id);
        }

        $scope.getaccokent =  function() {
            var token = angular.element("input[name='__RequestVerificationToken']").val();

            var config = {
                headers: {
                    '__RequestVerificationToken': token
                }
            };
            return config;
        }

        $scope.xacnhan = function () {

            if ($scope.products.length >= 1)
            {
              
                var config = $scope.getaccokent();
                $http.post('../../cart/xacnhan',
                      {
                          diachi: $scope.diachi,
                          dienthoai: $scope.dienthoai,
                          thoigian: $scope.thoigian,
                          chitiet: $scope.chitiets
                      }, config
                  ).
                success(function (data, status, headers, config) {
                    cart_service.clean();
                    $window.location.href = "../cart/thankyou?ID="+data;
                }).
                error(function (data, status, headers, config) {
                    alert("Lỗi khi xác nhận mua hàng!")
                });
            }
            else {
                alert("Giỏ hàng trống! Bạn cần chọn sản phẩm trước");
            }
        }

        $scope.converttochiviets = function () {
            $scope.chitiets = [];
            for (var i = 0; i < $scope.products.length; i++) {
                var item = {};
                item.id = $scope.products[i].id;
                item.soluong = $scope.products[i].number;
                
                item.idbosanpham = $scope.products[i].idbosanpham;
                $scope.chitiets.push(item);
            }
        }

        $scope.back = function () {
            $window.location.href = "/home";
        }

        $rootScope.$on('cart_index_controller::recount_amount', function () {
            $scope.counttotalamount();
        })

        $scope.AddAntiForgeryToken = function (data) {
            data.__RequestVerificationToken = $('#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
            return data;
        };
    }]);