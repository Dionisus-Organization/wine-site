<template>
    <div id="app">
        <header>
            <b-navbar toggleable="md" type="light">
                <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
                <b-navbar-brand to="/">Wines</b-navbar-brand>
                    <b-collapse is-nav id="nav-collapse">
                        <b-navbar-nav>
                            <b-nav-item href="#" @click.prevent="login" v-if="!user">Login</b-nav-item>
                            <b-nav-item href="#" @click.prevent="logout" v-else>Logout</b-nav-item>
                            <username v-if="user" :username="user.email"></username>
                            <b-nav-item @click.prevent="openUserPage" v-if="user">User Account</b-nav-item>
                        </b-navbar-nav>
                    </b-collapse>
            </b-navbar>
            <div class="main-header">
                <div class="site-header-container">
                    <div class="header-line"></div>
                    <div class="site-name-container">
                        <h1 class="site-name">
                            <router-link :to="{name: 'wineList'}" tag="a">
                            WINE RATING
                            </router-link>
                        </h1>
                    </div>
                    <div class="header-line"></div>
                </div>
            </div>
        </header>
        <router-view :user="user"></router-view>
    </div>
</template>

<script>
    import Username from "@/components/Username";
    export default {
        name: 'App',
        components: { Username },
        data() {
            return {
                user: null
            }
        },
        async created() {
            await this.refreshUser()
        },
        watch: {
            '$route': 'onRouteChange'
        },
        methods: {
            login() {
                this.$auth.loginRedirect()
            },
            async onRouteChange() {
                await this.refreshUser()
            },
            async refreshUser() {
                this.user = await this.$auth.getUser()
            },
            async logout() {
                await this.$auth.logout()
                await this.refreshUser()
                this.$router.push('/')
            },
            openUserPage() {
                this.$router.push('/components/UserAccount')
            }
        }
    }
</script>

<style lang="scss">
    body {
        font-family: $textFont;
        h1, h2, h3, h4, h5, h6 {
            font-family: $headerFont;
        }
        h1 {
            font-size: 1.7em;
        }
    }

    input:focus,
    select:focus,
    textarea:focus,
    button:focus {
        outline: none;
    }

    .main-header {
        background: $primaryDarkColor;
        color: $textColorOnColorBackground;
        padding: 25px 0 25px 0;
        .site-header-container {
            width: 500px;
            text-align: center;
            margin: 0 auto 0 auto;
            .header-line {
                width: 100%;
                border-top: 2px solid $textColorOnColorBackground;
            }
            .site-name-container {
                width: 480px;
                margin: 3px auto 3px auto;
                padding: 5px 0;
                border-top: 2px solid $textColorOnColorBackground;
                border-bottom: 2px solid $textColorOnColorBackground;
                h1.site-name {
                    margin: 0;
                    font-size: 64px;
                    a {
                        color: $textColorOnColorBackground;
                    }
                    a:hover {
                        text-decoration: none;
                        opacity: 0.7;
                    }
                }
            }
        }
    }

    .navigation {
        background: $primaryColor;
        color: $textColorOnColorBackground;
    }

    #nav {
    padding: 30px;

        a {
            font-weight: bold;
            color: #2c3e50;

            &.router-link-exact-active {
            color: #42b983;
            }
        }
    }
</style>