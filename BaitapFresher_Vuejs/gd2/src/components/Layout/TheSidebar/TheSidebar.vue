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
                    <span class="sidebar-tooltip">Tổng quan</span>
                    <div class="sidebar-hover"></div>
                </div>
                <router-link to="/Theemployee" class="sidebar-item">
                    <div class="sidebar-item-icon sicon2"></div>
                    <label class="sidebar-item-laber">Nhân viên</label>
                    <span class="sidebar-tooltip">Nhân viên</span>
                    <div class="sidebar-hover"></div>
                </router-link>
                <router-link to="/Theproduct" class="sidebar-item">
                    <div class="sidebar-item-icon sicon3"></div>
                    <label class="sidebar-item-laber">Danh sách hàng hóa</label>
                    <span class="sidebar-tooltip">Danh sách hàng hóa</span>
                    <div class="sidebar-hover"></div>
                </router-link>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon4"></div>
                    <label class="sidebar-item-laber">Mua hàng</label>
                    <span class="sidebar-tooltip">Mua hàng</span>
                    <div class="sidebar-hover"></div>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon5"></div>
                    <label class="sidebar-item-laber">Bán hàng</label>
                    <span class="sidebar-tooltip">Bán hàng</span>
                    <div class="sidebar-hover"></div>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon6"></div>
                    <label class="sidebar-item-laber">Quản lý hóa đơn</label>
                    <span class="sidebar-tooltip">Quản lý hóa đơn</span>
                    <div class="sidebar-hover"></div>
                </div>
                <router-link to="/Thewarehouse" class="sidebar-item">
                    <div class="sidebar-item-icon sicon7"></div>
                    <label class="sidebar-item-laber">Kho</label>
                    <span class="sidebar-tooltip">Kho</span>
                    <div class="sidebar-hover"></div>
                </router-link>
                <router-link to="/Theunit" class="sidebar-item">
                    <div class="sidebar-item-icon sicon8"></div>
                    <label class="sidebar-item-laber">Đơn vị tính</label>
                    <span class="sidebar-tooltip">Đơn vị tính</span>
                    <div class="sidebar-hover"></div>
                </router-link>
                <router-link to="/Thesupply" class="sidebar-item">
                    <div class="sidebar-item-icon sicon9"></div>
                    <label class="sidebar-item-laber">Nhóm VTHH</label>
                    <span class="sidebar-tooltip">Nhóm vật tư hàng hóa</span>
                    <div class="sidebar-hover"></div>
                </router-link>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon10"></div>
                    <label class="sidebar-item-laber">Thuế</label>
                    <span class="sidebar-tooltip">Thuế</span>
                    <div class="sidebar-hover"></div>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon11"></div>
                    <label class="sidebar-item-laber">Giá thành</label>
                    <div class="sidebar-hover"></div>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon12"></div>
                    <label class="sidebar-item-laber">Tổng hợp</label>
                    <div class="sidebar-hover"></div>
                </div>
                <div class="sidebar-item">
                    <div class="sidebar-item-icon sicon13"></div>
                    <label class="sidebar-item-laber">Ngân sách</label>
                    <div class="sidebar-hover"></div>
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
    padding-left: 12px;
    margin-top: 4px;
    border-radius: 4px;
    text-decoration: none;
    position: relative;
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
    
}.sidebar-tooltip{
    max-width: 120px;
    font-size: 11px;
    left: 60px;
    top: 45px;
    position: absolute;
    background-color: #505050;
    border-radius: 2px;
    padding: 2px 4px;
    z-index: 5;
    text-align: center;
    color: #fff;
    visibility: hidden;
}.sidebar-item:hover .sidebar-tooltip {
    visibility: visible;
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
}.sidebar-hover{
    visibility: hidden;
    background-color: #2CA01C; 
    position: absolute;
    left: 0;
    width: 5px; 
    height: 44px;
}.router-link-active .sidebar-hover{
    visibility: visible;
}
@media (max-width: 1200px) {
    .sidebar-logo{
    display: none;
    }.sidebar-btn-left{
        margin-left: 10px;

    }.sidebar-item{
        width: 38px;
        margin: 0;
    }.sidebar-item-laber{
        display: none;
    }.sidebar-item-icon{
        margin-right: 0;
    }.sidebar-tooltip{
        min-width: 60px;
        left: 50px;
    }

}

</style>
<script>
export default {
    setup() {
        
    }
}
</script>