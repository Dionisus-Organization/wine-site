<template>
    <div>
        <div class="title-container">
            <h3>Recommended wines</h3>
        </div>
        <div class="container" v-if="!isLoading">
            <WineRatesTable :items="wines" :showChecklist="false"/>
        </div>
        <p v-if="isLoading">Loading...</p>
    </div>
</template>

<script>
import WineRatesTable from '../components/WineRatesTable.vue'
import api from '@/httpService';

export default {
    name: "WineList",
    components: {
        WineRatesTable
    },
    data() {
        return {
            isLoading: true,
            wines: [],
            itemsCount: 20,
            checkedWines: [],
            globalItemsCount: 0,
            pageSize: 20,
            rec_list: []
        };
    },
    computed: {
        items() {
            console.log(this.wines)
            return this.wines;
        },
    },
    created: async function () {
        console.log(localStorage.getItem('checkedWines'));
        let wineIds = localStorage.getItem('checkedWines').split(',');
        await api.getRecommend(wineIds).then((data) => {
            this.isLoading = false;
            this.wines = data.data;
        })
    }
}
</script>

<style lang="scss">
.search-filters-container {
    text-align: center;
    background: $primaryColor;
    padding: 10px 0 20px 0;

    .search-filters-form {
        label select, input {
            background: $primaryLightColor;
            border: 1px solid $primaryColor;
            border-radius: 3px;
            color: $textColorOnColorBackground;
            padding: 5px 10px;
            margin: 0 5px 0 0;
            box-sizing: content-box;
            height: 30px;
        }

        label select:focus, select:hover, input:focus, input:hover, textarea:focus, textarea:hover {
            background: $primaryLightColorFocus;
            border: 1px solid $primaryColorFocus;
        }

        span.error-text {
            color: $textColorOnColorBackground;
            font-size: 0.7em;
            opacity: 0.8;
        }

        ::placeholder {
            color: $textColorOnColorBackground;
            opacity: 0.5;
        }

        button {
            box-sizing: content-box;
            border: none;
            height: 30px;
            padding: 5px 15px;
            border-radius: 3px;
            background: $primaryDarkColor;
            color: $textColorOnColorBackground;
        }

        button:focus, button:hover {
            background: $primaryDarkColorFocus;
        }
    }
}

nav ul.pagination {
    list-style-type: none;
    display: flex;
    justify-content: center;
    li.page-item.active {
        a.page-link {
            background-color: $primaryDarkColor;
            border-color: $primaryDarkColor;
            color: white;
        }
    }
    li {
        margin: 0 5px;
        display: list-item;
    }
}

button {
    margin: 0px 5px;
}

.title-container {
    text-align: center;
    background: $primaryColor;
    height: 80px;
    color: $textColorOnColorBackground;
    display: flex;
    justify-content: center;
    flex-direction: column;
}
</style>
