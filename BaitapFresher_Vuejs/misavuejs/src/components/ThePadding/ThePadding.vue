<template>
    <div class="padding">
                    <div class="padding-left" v-if="TotalCount">
                        <label>Tổng số: </label>
                        <label class="padding-left-bold">{{TotalCount.totalCount}}</label>
                        <label> bản ghi</label>
                    </div>
                    <div class="padding-right">
                        <div class="drop-box">
                            <MDropBox @padding-value ="getPaddingValue"/>
                        </div>
                        <div class="padding-center">
                            <button class="btn-first">Trước</button>
                            <div class="page-group">
                                <button class="paddingnumber button-seleced">1</button>
                                <button class="paddingnumber">2</button>
                                <button class="paddingnumber">3</button>
                            </div>
                            <div class="page-group-last">
                                <label >...</label>
                                <button class="paddingnumber">52</button>
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
}.padding-right select{
    width: 200px;
    height: 30px;
}.padding-right select:focus{
    border: 2px solid #019160;
    outline: none;
}
.padding-left{
    position: absolute;
    left: 0;
    margin-left: 10px;
}.padding-left-bold{
    font-weight: bold;
}
.page-group button{
    border: 1px solid #000;
    height: 25px;
    background-color: #fff;
}
.page-group button:hover{
    background-color: #fff;
}
.page-group .paddingnumber.button-seleced {
    font-weight: bold;
    border: 1px solid black;

}
.page-group .paddingnumber, .page-group-last .paddingnumber{
    width: 30px;
    height: 30px;
    cursor: pointer;
    border: none;
    outline: none;
    background-color: #fff;
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
}.drop-box{
    width: 200px;
    height: 30px;
    border: 1px solid #bbbbbb;
    outline: none;
}

</style>

<script>
import MDropBox from "./MDropBox.vue";
export default {
    props:{
        TotalCount: Object
    },
    components: {
        MDropBox
    },
    methods:{
         getPaddingValue(value){
             this.$emit("filter-padding", value);
         }   
    }, 
    data(){
        return{
            
        }
    }
}
</script>