using Game;
using Game.Prefabs;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;

namespace DepotCapacityChanger
{
    public partial class DepotCapacityChangerSystem : GameSystemBase
    {
        private Dictionary<Entity, TransportDepotData> _depotToData = new Dictionary<Entity, TransportDepotData>();

        private EntityQuery _query;

        protected override void OnCreate()
        {
            base.OnCreate();

            _query = GetEntityQuery(new EntityQueryDesc()
            {
                All = new[] {
                    ComponentType.ReadWrite<TransportDepotData>()
                }
            });

            RequireForUpdate(_query);
        }

        protected override void OnUpdate()
        {
            var depots = _query.ToEntityArray(Allocator.Temp);

            foreach (var depot in depots)
            {
                TransportDepotData data;

                if (!_depotToData.TryGetValue(depot, out data))
                {
                    data = EntityManager.GetComponentData<TransportDepotData>(depot);
                    _depotToData.Add(depot, data);
                }

                double scalar = data.m_TransportType switch
                {
                    TransportType.Bus => Mod.m_Setting.BusSlider,
                    TransportType.Taxi => Mod.m_Setting.TaxiSlider,
                    TransportType.Subway => Mod.m_Setting.SubwaySlider,
                    TransportType.Tram => Mod.m_Setting.TramSlider,
                    TransportType.Train => Mod.m_Setting.TrainSlider,
                    _ => 0
                } / 100d;

                if (scalar == 0) continue;

                data.m_VehicleCapacity = (int)(scalar * data.m_VehicleCapacity);
                EntityManager.SetComponentData<TransportDepotData>(depot, data);
            }

            Enabled = false;
        }
    }
}

