<template>
    <div class="header">
            <div class="header-left">
                <button type="button" class="header-left-btn header-left-btn-menu"></button>
                <label class="header-left-label">CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</label>
                <button type="button" class="header-left-btn header-left-btn-down"></button>
            </div>
            <div class="header-right">
                <button type="button" class="header-right-btn header-notification"></button>
                <div class="header-avatar"></div>
                <label class="header-username">Nguyễn Văn Cương</label>
                <button type="button" class="header-right-btn header-right-btn-down"></button>
            </div>
        </div>
</template>
<style>

.header-left{
    width: calc(100%-300px);
    height: 48px;
    display: flex;
    align-items: center;
    flex-direction: row;
    float: left;
    column-gap: 10px;
    margin-left: 10px;
}
.header-left-btn{
    width: 24px;
    height: 24px;
    border: none;
   
}
.header-left-btn-menu{
    background-image: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\img\Sprites.64af8f61.svg);
    background-repeat: no-repeat;
    background-position: -845px -32px;
    background-color: #fff;
    /* background: url(/Cuong_BaitapFresher_CTM/assets/Resource/img/Sprites.64af8f61.svg) no-repeat -845px -32px; */

}
.header-left-btn-down{
    background-image: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\img\Sprites.64af8f61.svg);    background-repeat: no-repeat;
    background-position: -943px -357px;
    background-color: #fff;
    /* background: url(/Cuong_BaitapFresher_CTM/assets/Resource/img/Sprites.64af8f61.svg) no-repeat -943px -357px; */
   
}
.header-right{
    width: 250px;
    height: 48px;
    display: flex;
    float: right;
    flex-direction: row;
    align-items: center;
    margin-right: 20px;

}
.header-right-btn{
    width: 24px;
    height: 24px;
    background-size: 16px 16px;
    border: none;
    background-color: #fff;
    background-position: 16px center;
}
.header-notification{
    background: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\img\Sprites.64af8f61.svg) no-repeat -258px -32px;
}
.header-avatar{
    width: 37px;
    height: 32px;
    border-radius: 50%;
    margin-left: 30px;
 
    background: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\img\Sprites.64af8f61.svg) no-repeat -353px -893px;

}
.header-right-btn-down{
    background: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\img\Sprites.64af8f61.svg) no-repeat -943px -357px;
}
.header-username{
    width: 180px;
    font-weight: bolder;
    font-size: medium;
    text-align: center;
}.header-left-label{
    font-weight: bold;
    margin-left: 5px;
}

</style>

<script>

</script>