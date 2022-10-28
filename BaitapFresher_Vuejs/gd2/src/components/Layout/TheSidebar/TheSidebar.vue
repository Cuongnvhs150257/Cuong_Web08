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
                <router-link to="/Theemployee" class="sidebar-item">
                    <div class="sidebar-item-icon sicon2"></div>
                    <label class="sidebar-item-laber">Tiền mặt</label>
                    
                </router-link>
                <router-link to="/Theproduct" class="sidebar-item">
                    <div class="sidebar-item-icon sicon3"></div>
                    <label class="sidebar-item-laber">Tiền gửi</label>
                    
                </router-link>
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
                <router-link to="/Thewarehouse" class="sidebar-item">
                    <div class="sidebar-item-icon sicon7"></div>
                    <label class="sidebar-item-laber">Kho</label>
                </router-link>
                <router-link to="/Theunit" class="sidebar-item">
                    <div class="sidebar-item-icon sicon8"></div>
                    <label class="sidebar-item-laber">Công cụ dụng cụ</label>
                </router-link>
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

            </div>
    </div>
</template>
<style>
:root{
  --icon: url('../../../assets/Resource/img/Sprites.64af8f61.svg');
  --logo: url('../../../assets/Resource/img/Logo_Module_TiengViet_White.66947422.svg'); 
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
    margin-left: 20px;
    margin-top: 14px;
}
.sidebar-btn-left-open{
    background-image: var(--icon);
    background-repeat: no-repeat;
    background-position: -418px -92px;
    background-color: #393A3D;
    
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
    width: 180px;
    height: calc(100vh - 52px); 
}
.sidebar-item{
    width: 176px;
    height: 44px;
    display: flex;
    cursor: pointer;
    justify-content: center;
    margin-left: 12px;
    margin-top: 4px;
    border-radius: 4px;
    text-decoration: none;
}
.sidebar-item:hover {
    background-color: rgb(88,89,93);
}
.sidebar-item:hover .sidebar-item-icon{
    background-color: rgb(88,89,93);
}
.sidebar-item-icon{
    width: 24px;
    height: 24px;
    margin-right: 12px;
    margin-top: 14px;
    
}

.sidebar-item-laber{
    color: #fff;
    width: 130px;
    margin-top: 15px;
    font-size: 13px;

}
.sicon1{
    background-image: var(--icon);
    background-position: -28px -1630px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon2{
    background-image: var(--icon);
    background-position: -73px -1631px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon3{
    background-image: var(--icon);
    background-position: -117px -1632px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon4{
    background-image: var(--icon);
    background-position: -161px -1632px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}.sicon5{
    background-image: var(--icon);
    background-position: -204px -1631px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon6{
    background-image: var(--icon);
    background-position: -248px -1632px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon7{
    background-image: var(--icon);
    background-position: -289px -1632px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon8{
    background-image: var(--icon);
    background-position: -334px -1633px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon9{
    background-image: var(--icon);
    background-position: -377px -1632px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon10{
    background-image: var(--icon);
    background-position: -421px -1634px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon11{
    background-image: var(--icon);
    background-position: -467px -1633px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon12{
    background-image: var(--icon);
    background-position: -510px -1632px;        
    background-repeat: no-repeat;
    background-color: #393A3D;
}
.sicon13{
    background-image: var(--icon);
    background-position: -377px -1664px;
    background-repeat: no-repeat;
    background-color: #393A3D;
}
@media (max-width: 1200px) {
    .sidebar-logo{
    display: none;
    }.sidebar-btn-left{
        margin-left: 10px;

    }.sidebar-item{
        width: 56px;
        margin: 0;
    }.sidebar-item-laber{
        display: none;
    }.sidebar-item-icon{
        margin-right: 0;
    }

}

</style>
<script>
export default {
    setup() {
        
    }
}
</script>