<template>
    <div>
        <div class="search-filters-container">
            <form @submit.prevent="submitForm" class="search-filters-form">
                <label for="color">
                    <select name="color" id="color" v-model="form.color">
                        <option value="" disabled selected>Color</option>
                        <option value="red">Red</option>
                        <option value="white">White</option>
                        <option value="pink">Pink</option>
                    </select>
                </label>
                <label for="wine-type">
                    <select name="wine-type" id="wine-type" v-model="form.wineType">
                        <option value="" disabled selected>Type</option>
                        <option value="dry">Dry</option>
                        <option value="sweet">Sweet</option>
                    </select>
                </label>
                <label for="country">
                    <input name="country" id="country" v-model="form.country" placeholder="Country"/>
                </label>
                <ValidationProvider :rules="{between: {min: form.minVintage, max: form.maxVintage}}"
                                    v-slot="{ errors }">
                    <label for="vintage" style="position: relative">
                        <input name="vintage" id="vintage" v-model="form.vintage"
                               placeholder="Vintage" type="number" :min='form.minVintage' :max='form.maxVintage'/>
                        <span class="error-text " v-if="errors[0]" style="position: absolute; bottom: -20px; left: 3px">
                                Vintage is not available
                            </span>
                    </label>
                </ValidationProvider>
                <button type="submit" :disabled="isLoading">Show</button>
            </form>
        </div>
        <div class="container" v-if="!isLoading">
            <WineRatesTable :items="items" @toParent="getCheckedWines" :showChecklist="true"/>
            <RouterLink :to="{name: 'recommend'}">
                <button type="button" v-on:click="recommend" :disabled="!checkedWines.length" class="btnRec" v-bind:class="{ disabled: !checkedWines.length }">Recommend</button>
            </RouterLink>
            <nav>
                <paginate v-model="winePage"
                        :page-count="globalItemsCount"
                        class="pagination"
                        :prev-class="'page-item'"
                        :prev-link-class="'page-link'"
                        :next-class="'page-item'"
                        :next-link-class="'page-link'"
                        :prev-text="'Prev'"
                        :next-text="'Next'"
                        :container-class="'pagination'"
                        :page-class="'page-item'"
                        :page-link-class="'page-link'"
                        :click-handler="clickCallback">
                </paginate>
            </nav>
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
                form: {
                    color: '',
                    wineType: '',
                    country: '',
                    vintage: '',
                    minVintage: 1863,
                    maxVintage: new Date().getFullYear(),
                },
                winePage: 1,
                isLoading: true,
                isWinesChecked: false,
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
                return this.wines;
            },
        },
        created: async function () {
            await api.getAll(this.winePage).then((data) => {
                this.isLoading = false;
                this.wines = data;
            })
            await api.getCount().then((data) => this.globalItemsCount = Math.floor(data / this.pageSize))
        },
        methods: {
            submitForm() {
                api.getFilter(
                    this.changeCase(this.form.color),
                    this.form.wineType,
                    this.changeCase(this.form.country),
                    this.form.vintage)
                    .then((data) => {
                        console.log(data.data);
                        this.wines = data.data.wineList;
                        this.globalItemsCount = Math.floor(data.data.count / this.pageSize)
                })
            },
            clickCallback(pageNum) {
                this.winePage = pageNum;
                api.getAll(pageNum).then((data) => {
                    this.isLoading = false;
                    this.wines = data;
                })
            },
            recommend() {
                let checkedWines_id = this.checkedWines.map(item => item.id);
                if (checkedWines_id) {
                    localStorage.setItem('checkedWines', checkedWines_id.join());
                } else {
                    localStorage.removeItem('checkedWines');
                }
            },
            getCheckedWines(value) {
                this.checkedWines = value;
            },
            changeCase(word) {
                if(word) {
                    return word[0].toUpperCase() + word.slice(1).toLowerCase()
                }
            }
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

            button:focus, button:hover {
                background: $primaryDarkColorFocus;
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
        }
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

    .btnRec {
        float: right
    }

    .disabled {
        background-color: gray;
    }

    nav ul.pagination {
        list-style-type: none;
        display: flex;
        justify-content: center;
        width: 100%;
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
</style>
