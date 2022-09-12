<template>
    <div class="sidebar">
            <div class="sidebar-top">
                <button class="sidebar-btn-left sidebar-btn-left-open" type="button"></button>
                <div class="sidebar-logo"></div>
            </div>
            <div class="sidebar-menu">
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon1"></div>
                    <label class="sidebar-item-laber">Tổng quan</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon2"></div>
                    <label class="sidebar-item-laber">Tiền mặt</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon3"></div>
                    <label class="sidebar-item-laber">Tiền gửi</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon4"></div>
                    <label class="sidebar-item-laber">Mua hàng</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon5"></div>
                    <label class="sidebar-item-laber">Bán hàng</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon6"></div>
                    <label class="sidebar-item-laber">Quản lý hóa đơn</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon7"></div>
                    <label class="sidebar-item-laber">Kho</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon8"></div>
                    <label class="sidebar-item-laber">Công cụ dụng cụ</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon9"></div>
                    <label class="sidebar-item-laber">Tài sản cố định</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon10"></div>
                    <label class="sidebar-item-laber">Thuế</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon11"></div>
                    <label class="sidebar-item-laber">Giá thành</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon12"></div>
                    <label class="sidebar-item-laber">Tổng hợp</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon13"></div>
                    <label class="sidebar-item-laber">Ngân sách</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon14"></div>
                    <label class="sidebar-item-laber">Báo cáo</label>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon15"></div>
                    <label class="sidebar-item-laber">Phân tích tài chính</label>
                </div>

            </div>
    </div>
</template>
<style>
:root{
  --icon: url('http://localhost:8080/img/Sprites.64af8f61.2ff7618a.svg');
  --logo: url('http://localhost:8080/img/Logo_Module_TiengViet_White.66947422.abf1e380.svg');
}
    .sidebar-top{
    width: 200px;
    height: 48px;
    display: flex;
    align-items: center;
}
.sidebar-btn-left{
    width: 30px;
    height: 30px;
    border: none;
    margin-left: 27px;
    margin-top: 10px;
}
.sidebar-btn-left-open{
    background-image: var(--icon);
    background-repeat: no-repeat;
    background-position: -418px -92px;
    background-color: #041434;
    
}
.sidebar-logo{
    width: 150px;
    height: 48px;
    background-image: var(--logo);
    background-repeat: no-repeat;
    background-size: 100px;
    background-position: 16px center;
    
}
.sidebar-menu{
    width: 200px;
    height: calc(100vh - 48px); 
    margin-top: 5px;
}
.sidebar-item{
    width: 200px;
    height: 40px;
    display: flex;
    cursor: pointer;
    justify-content: center;
}
.sidebar-item:hover {
    background-color: rgb(88,89,93);
}
.sidebar-item:hover .sidebar-item-icon{
    background-color: rgb(88,89,93);
}
.sidebar-item-icon{
    width: 20px;
    height: 20px;
    margin-right: 10px;
    margin-top: 10px;
    
}

.sidebar-item-laber{
    color: #fff;
    width: 130px;
    margin-top: 10px;

}
.sicon1{
    background-image: var(--icon);
    background-position: -34px -1630px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon2{
    background-image: var(--icon);
    background-position: -78px -1631px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon3{
    background-image: var(--icon);
    background-position: -121px -1633px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon4{
    background-image: var(--icon);
    background-position: -165px -1632px;
    background-repeat: no-repeat;
    background-color: #041434;
}.sicon5{
    background-image: var(--icon);
    background-position: -208px -1631px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon6{
    background-image: var(--icon);
    background-position: -254px -1632px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon7{
    background-image: var(--icon);
    background-position: -294px -1632px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon8{
    background-image: var(--icon);
    background-position: -339px -1633px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon9{
    background-image: var(--icon);
    background-position: -382px -1632px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon10{
    background-image: var(--icon);
    background-position: -427px -1634px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon11{
    background-image: var(--icon);
    background-position: -471px -1633px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon12{
    background-image: var(--icon);
    background-position: -514px -1632px;        
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon13{
    background-image: var(--icon);
    background-position: -382px -1664px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon14{
    background-image: var(--icon);
    background-position: -552px -1636px;
    background-repeat: no-repeat;
    background-color: #041434;
}
.sicon15{
    background-image: var(--icon);
    background-position: -207px -1667px;
    background-repeat: no-repeat;
    background-color: #041434;
}


</style>
<script>
export default {
    setup() {
        
    },
}
</script>