import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        AsideMenu: require('@/home/components/menu/aside-menu/aside-menu.vue').default,
        TopMenu: require('@/home/components/menu/top-menu/top-menu.vue').default
    }
})
export default class AdminLayoutComponent extends NucleusComponentBase {
    public created() {
        this.nucleus.auth.fillProps();
    }
}