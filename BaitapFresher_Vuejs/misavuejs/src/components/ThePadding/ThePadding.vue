<template>
    <div class="padding">
                    <div class="padding-left">Tổng số: 1035 bản ghi</div>
                    <div class="padding-right">
                        <select >
                            <option value="1">20 bản ghi trên 1 trang</option>
                            <option value="2">20 nhân viên/trang</option>
                        </select>
                        <div class="padding-center">
                            <button class="btn-first">Trước</button>
                            <div class="page-group">
                                <button class="paddingnumber button-seleced">1</button>
                                <button class="paddingnumber">2</button>
                                <button class="paddingnumber">3</button>
                                <button class="paddingnumber">4</button>
                            </div>
                            <button class="btn-last">Sau</button>
                        </div>
                    </div>
                </div>
</template>

<style>
.padding{
    width: 100%;
    height: 60px;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    order: 5;
}
.padding-center{
    display: flex;
    margin-left: 20px;
    
}.padding-right{
    position: absolute;
    right: 0;
    margin-right: 10px;
    display: flex;
    height: 25px;
}.padding-left{
    position: absolute;
    left: 0;
    margin-left: 10px;
}
.page-group button{
    border: 1px solid #000;
    height: 25px;
    background-color: #fff;
}
.page-group button:hover{
    background-color: #fff;
}
.button-seleced {
    background-color: #019160;
    font-weight: bold;

}
.paddingnumber{
    width: 30px;
    height: 30px;
    margin: 2px;
    cursor: pointer;
}
.btn-first, .btn-last{
    width: 50px;
    height: 30px;
    border: none;
    background-color: #fff;
    cursor: pointer;
}
.btn-first:hover, .btn-last:hover{
    color: #666666;
}
</style>

<script>
export default {
    setup() {
        
    },
}
</script>